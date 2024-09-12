using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using MediatR;

namespace Mango.Services.CouponAPI.CQRS.Handlers
{
    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, ResponseDto>
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDto _response;
        public CreateCouponCommandHandler(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }
        public async Task<ResponseDto> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(request);
                await _db.Coupons.AddAsync(coupon);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<CouponDto>(coupon);
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
