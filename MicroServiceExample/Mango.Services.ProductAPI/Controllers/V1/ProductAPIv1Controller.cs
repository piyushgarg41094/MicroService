﻿using Asp.Versioning;
using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Filters;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers.V1
{
    //to run api through url
    [Route("api/v{version:apiVersion}/product")]
    //to run api through header
    //[Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductAPIv1Controller : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public ProductAPIv1Controller(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        [TransactionFilter]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _db.Products.ToList();
                var ans = new List<Product>() { objList.LastOrDefault() };
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(ans);
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
