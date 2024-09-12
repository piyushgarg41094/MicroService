using Mango.Services.CouponAPI.Models.Dto;
using MediatR;

namespace Mango.Services.CouponAPI.CQRS
{
    public class GetCouponByIdQuery : IRequest<ResponseDto>
    {
        public int Id { get; set; }
    }
}
