using Mango.Web.Models;

namespace Mango.Web.ExternalService.Interfaces
{
    public interface ICouponServiceRefit
    {
        Task<BaseServiceResponse<Guid>> GetCouponsBycode(string code);
    }
}
