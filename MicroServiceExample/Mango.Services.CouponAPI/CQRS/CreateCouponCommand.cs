using Mango.Services.CouponAPI.Models.Dto;
using MediatR;

namespace Mango.Services.CouponAPI.CQRS
{
    public class CreateCouponCommand : IRequest<ResponseDto>
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
