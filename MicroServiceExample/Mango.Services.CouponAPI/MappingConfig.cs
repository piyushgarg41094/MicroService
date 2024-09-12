using AutoMapper;
using Mango.Services.CouponAPI.CQRS;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
                config.CreateMap<Coupon, CreateCouponCommand>();
                config.CreateMap<CreateCouponCommand, Coupon>();
                config.CreateMap<CouponDto, CreateCouponCommand>();
            }); 
            return mappingConfig;
        }
    }
}
