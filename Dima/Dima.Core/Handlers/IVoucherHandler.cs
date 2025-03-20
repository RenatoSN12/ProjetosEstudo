using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Requests.Responses;

namespace Dima.Core.Handlers;

public interface IVoucherHandler
{
    Task<Response<Voucher?>> GetByCodeAsync(GetVoucherByCodeRequest request);
}