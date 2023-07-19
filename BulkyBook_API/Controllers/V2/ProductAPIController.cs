using AutoMapper;
using BulkyBook_API.Models;
using BulkyBook_API.Repository.IRepostiory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/ProductAPI")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ProductAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IProductRepository _dbProduct;
        private readonly ICategoryRepository _dbCategory;
        private readonly IMapper _mapper;
        public ProductAPIController(IProductRepository dbProduct, IMapper mapper,
            ICategoryRepository dbCategory)
        {
           _dbProduct = dbProduct;
            _mapper = mapper;
            _response = new();
            _dbCategory = dbCategory;
        }


        //[MapToApiVersion("2.0")]
        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Hardik", "HardikKanzariya" };
        }


    }
}
