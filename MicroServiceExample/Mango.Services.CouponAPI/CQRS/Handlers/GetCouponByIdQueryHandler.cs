using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.CQRS.Handlers
{
    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, ResponseDto>
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDto _response;
        public GetCouponByIdQueryHandler(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        public async Task<ResponseDto> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var coupons = await _db.Coupons.FirstAsync(c => c.CouponId == request.Id);
                _response.Result = _mapper.Map<CouponDto>(coupons);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
