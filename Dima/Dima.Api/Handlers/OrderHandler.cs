using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Requests.Stripe;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class OrderHandler(AppDbContext context, IStripeHandler stripeHandler) : IOrderHandler
{
    public async Task<Response<Order?>> CancelAsync(CancelOrderRequest request)
    {
        Order? order;

        try
        {
            order = await context.Orders.Include(x => x.Product)
                .Include(x => x.Voucher)
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (order == null)
                return new Response<Order?>(null, 404, "Pedido não encontrado");

            switch (order.Status)
            {
                case EOrderStatus.Cancelled:
                    return new Response<Order?>(order, 400, "Este pedido já foi cancelado");

                case EOrderStatus.WaitingPayment:
                    break;

                case EOrderStatus.Paid:
                    return new Response<Order?>(null, 400, "Este pedido já foi pago e não pode ser cancelado.");

                case EOrderStatus.Refunded:
                    return new Response<Order?>(null, 400,
                        "Este pedido já foi reembolsado e não pode mais ser cancelado.");

                default:
                    return new Response<Order?>(null, 400, "Este pedido não pode ser cancelado.");
            }

            order.Status = EOrderStatus.Cancelled;
            order.UpdatedAt = DateTime.Now;

            try
            {
                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }
            catch
            {
                return new Response<Order?>(order, 500, "Não foi possível cancelar o pedido");
            }
        }
        catch
        {
            return new Response<Order?>(null, 500, "Erro ao obter o pedido");
        }

        return new Response<Order?>(order, 200, $"Pedido {order.Number} cancelado com sucesso");
    }

    public async Task<Response<Order?>> CreateAsync(CreateOrderRequest request)
    {
        Product? product;
        try
        {
            product = await context.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ProductId && x.IsActive);

            if (product == null)
                return new Response<Order?>(null, 400, "Produto não encontrado.");

            context.Attach(product);
        }
        catch
        {
            return new Response<Order?>(null, 500, "Erro ao obter o pedido");
        }

        Voucher? voucher = null;
        try
        {
            if (request.VoucherId is not null)
            {
                voucher = await context.Vouchers.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.VoucherId && x.IsActive);

                if (voucher == null)
                    return new Response<Order?>(null, 400, "Voucher inválido ou não encontrado.");


                if (!voucher.IsActive)
                    return new Response<Order?>(null, 400, "Esse voucher já foi utilizado.");

                voucher.IsActive = false;
                context.Vouchers.Update(voucher);
            }
        }
        catch
        {
            return new Response<Order?>(null, 500, "Erro ao obter o voucher informado.");
        }

        var order = new Order
        {
            UserId = request.UserId,
            Product = product,
            ProductId = request.ProductId,
            Voucher = voucher,
            VoucherId = request.VoucherId
        };

        try
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }
        catch
        {
            return new Response<Order?>(order, 500, "Não foi possível realizar seu pedido.");
        }

        return new Response<Order?>(order, 201, $"Pedido {order.Number} cadastrado com sucesso");
    }

    public async Task<Response<Order?>> PayAsync(PayOrderRequest request)
    {
        Order? order;
        try
        {
            order = await context.Orders
                .Include(x => x.Product)
                .Include(x=>x.Voucher)
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Number == request.OrderNumber);
            if (order == null)
                return new Response<Order?>(null, 404, "Pedido não encontrado");
        }
        catch
        {
            return new Response<Order?>(null, 500, "Erro ao obter o pedido");
        }

        switch (order.Status)
        {
            case EOrderStatus.Cancelled:
                return new Response<Order?>(order, 400, "Este pedido já foi cancelado e não pode ser pago.");
            case EOrderStatus.Paid:
                return new Response<Order?>(order, 400, "Este pedido já está pago!");
            case EOrderStatus.Refunded:
                return new Response<Order?>(order, 400, "Este pedido já foi reembolsado e não pode ser pago.");
            case EOrderStatus.WaitingPayment:
                break;
            default:
                return new Response<Order?>(order, 400, "Não foi possível pagar o pedido");
        }

        try
        {
            var getTransactionsRequest = new GetTransactionByOrderNumberRequest
            {
                Number = order.Number
            };
            
            var result = await stripeHandler.GetTransactionsByOrderNumberAsync(getTransactionsRequest);
            if(!result.IsSuccess)
                return new Response<Order?>(null, 500, "Não foi possível localizar o pagamento do pedido!");
            
            if(result.Data is null)
                return new Response<Order?>(null, 500, "Não foi possível localizar o pagamento do pedido!");

            if (result.Data.Any(x => x.Refunded))
                return new Response<Order?>(null, 500, "Este pedido já teve o pagamento informado");
            
            if (!result.Data.Any(x => x.Paid))
                return new Response<Order?>(null, 500, "Este pedido não foi pago");

            request.ExternalReference = result.Data[0].Id;
        }
        catch (Exception ex)
        {
            return new Response<Order?>(null, 500, "Não foi possível dar baixa no seu pedido!");
        }

        order.Status = EOrderStatus.Paid;
        order.ExternalReference = request.ExternalReference;
        order.UpdatedAt = DateTime.Now;

        try
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
        catch
        {
            return new Response<Order?>(order, 500, "Erro ao tentar realizar o pagamento do pedido.");
        }

        return new Response<Order?>(order, 200, $"Pedido {order.Number} pago com sucesso.");
    }

    public async Task<Response<Order?>> RefundAsync(RefundOrderRequest request)
    {
        Order? order;
        try
        {
            order = await context.Orders
                .Include(x => x.Product)
                .Include(x=>x.Voucher)
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);

            if (order == null)
                return new Response<Order?>(null, 400, "Pedido não encontrado");
        }
        catch
        {
            return new Response<Order?>(null, 500, "Não foi possível recuperar o pedido.");
        }

        switch (order.Status)
        {
            case EOrderStatus.Cancelled:
                return new Response<Order?>(order, 400, "Este pedido já foi cancelado e não pode ser estornado.");
            case EOrderStatus.Paid:
                break;
            case EOrderStatus.Refunded:
                return new Response<Order?>(order, 400, "Este pedido já foi reembolsado.");
            case EOrderStatus.WaitingPayment:
                return new Response<Order?>(order, 400, "Este pedido ainda não foi pago.");
            default:
                return new Response<Order?>(order, 400, "Não foi possível pagar o pedido");
        }

        order.Status = EOrderStatus.Refunded;
        order.UpdatedAt = DateTime.Now;

        try
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
        catch
        {
            return new Response<Order?>(order, 500, "Falha ao reembolsar o pagamento.");
        }

        return new Response<Order?>(order, 200, $"Pedido {order.Number} reembolsado com sucesso.");
    }

    public async Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrdersRequest request)
    {
        try
        {
            var query = context.Orders
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Voucher)
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.CreatedAt);

            var orders = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();
            return new PagedResponse<List<Order>?>(orders, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<Order>?>(null, 500, "Não foi possível recuperar os pedidos do usuário.");
        }
    }

    public async Task<Response<Order?>> GetByNumberAsync(GetOrderByNumberRequest request)
    {
        try
        {
            var order = await context.Orders
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Voucher)
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Number == request.Number);

            return order is null
                ? new Response<Order?>(null, 404, "Pedido não encontrado.")
                : new Response<Order?>(order);

        }
        catch (Exception e)
        {
            return new Response<Order?>(null, 500, "Não foi possível obter o pedido.");
        }
    }
}