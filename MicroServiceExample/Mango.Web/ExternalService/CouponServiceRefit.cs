using Mango.Web.ExternalService.Interfaces;
using Mango.Web.Models;
using System.Data;

namespace Mango.Web.ExternalService
{
    public class CouponServiceRefit(ICouponServiceClient couponServiceClient, ILogger<CouponServiceRefit> logger) : ICouponServiceRefit
    {
        public async Task<BaseServiceResponse<Guid>> GetCouponsBycode(string code)
        {
            return await ExecuteApiCall(() => couponServiceClient.GetCouponsBycode(code), "Error fetching Coupon.");
        }

        private async Task<BaseServiceResponse<T>> ExecuteApiCall<T>(Func<Task<T>> apiCall, string errorMessage)
        {
            try
            {

                var result = await apiCall();

                return new BaseServiceResponse<T>
                {
                    Data = result
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, errorMessage);

                return new BaseServiceResponse<T>()
                {
                    Success = false,
                    Message = errorMessage,
                    Errors = [ex.Message]
                };


            }

        }
    }
}
