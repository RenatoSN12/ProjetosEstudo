using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Requests.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class OrderHandler(AppDbContext context) : IOrderHandler
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

        Voucher? voucher;
        try
        {
            if (request.VoucherId is not null)
            {
                voucher = await context.Vouchers.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.VoucherId && x.IsActive);

                if (voucher == null)
                    return new Response<Order?>(null, 400, "Voucher inválido ou não encontrado.");

                voucher.IsActive = false;
                context.Vouchers.Update(voucher);
            }
        }
        catch
        {
            return new Response<Order?>(null, 500, "Erro ao obter o voucher informado.");
        }
    }

    public Task<Response<Order?>> PayAsync(PayOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Order?>> RefundAsync(RefundOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrdersRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Order?>> GetByNumber(GetOrderByNumberRequest request)
    {
        throw new NotImplementedException();
    }
}