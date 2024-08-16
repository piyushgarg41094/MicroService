using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    //[Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        //private readonly IConnectionMultiplexer _redis;
        private readonly IMemoryCache _cache;
        public CouponAPIController(AppDbContext db, IMapper mapper, 
            //IConnectionMultiplexer redis, 
            IMemoryCache cache)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
            //_redis = redis;
            _cache = cache;
        }

        //[HttpGet("GetThroughRedis")]
        //public ResponseDto GetThroughRedis()
        //{
        //    var db = _redis.GetDatabase();

        //    //redis cache key
        //    string redisKey = "redisKey";

        //    //Set a cache value
        //    db.StringSet(redisKey, "Hello Redis");

        //    //Get the Cache Value
        //    var value = db.StringGet(redisKey);

        //    //set cache with expiration time
        //    db.StringSet(redisKey, "Hello Redis", TimeSpan.FromMinutes(5));

        //    //remove cache
        //    db.KeyDelete(redisKey);

        //    return _response;
        //}


        [HttpGet]
        public ResponseDto Get()
        {
            var key = "cacheData";
            try
            {
                if (_cache.TryGetValue(key, out var value))
                {
                    _response.Result = value;
                }
                else
                {
                    IEnumerable<Coupon> objList = _db.Coupons.ToList();
                    value = _mapper.Map<IEnumerable<CouponDto>>(objList);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5)) // Keep in cache for 5 minutes
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1)); // Absolute expiration after 1 hour

                    // Save data in cache
                    _cache.Set(key, value, cacheEntryOptions);
                    _response.Result = value;
                }   
            }
            catch (Exception ex)
            {
                RemoveCache(key);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        private void RemoveCache(string key)
        {
            _cache.Remove(key);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();
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
