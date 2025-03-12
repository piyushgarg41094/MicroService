using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Mango.Web.ExternalService
{
    [Headers("accept: application/json")]
    public interface ICouponServiceClient
    {
        [HttpGet("GetByCode/{code}")]
        Task<Guid> GetCouponsBycode(string code);
    }
}
