using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Requests.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class VoucherHandler(AppDbContext context) : IVoucherHandler
{
    public async Task<Response<Voucher?>> GetByCodeAsync(GetVoucherByCodeRequest request)
    {
        try
        {
            var voucher = await context.Vouchers.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Code == request.Code && x.IsActive);

            return voucher is null
                ? new Response<Voucher?>(null, 500, "Voucher não encontrado.")
                : new Response<Voucher?>(voucher);
        }
        catch
        {
            return new Response<Voucher?>(null, 500, "Não foi possível recuperar o voucher.");
        }
    }
}