using AutoMapper;
using BulkyBook_API.Models;
using BulkyBook_API.Models.Dto;
using BulkyBook_API.Repository.IRepostiory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BulkyBook_API.Controllers.v1
{

    [Route("api/v{version:apiVersion}/OrderDetailAPI/[Action]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class OrderDetailAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IOrderDetailRepository _dbOrderDetail;
        private readonly IOrderHeaderRepository _dbOrderHeader;
        private readonly IProductRepository _dbProduct;
        private readonly IMapper _mapper;
        public OrderDetailAPIController(IOrderDetailRepository dbOrderDetail,
            IMapper mapper, IProductRepository dbProduct,
            IOrderHeaderRepository dbOrderHeader)
        {

            _mapper = mapper;
            _response = new();
            _dbOrderDetail = dbOrderDetail;
            _dbOrderHeader = dbOrderHeader;
            _dbProduct = dbProduct;
        }


        [HttpGet("{id:int}",Name = "GetOrderDetails")]
        // [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetOrderDetails(int id)
        {
            try
            {
                List<OrderDetail> OrderDetailList = await _dbOrderDetail.
                    GetAllAsync(u => u.OrderHeaderId == id,includeProperties: "OrderHeader,Product");

                _response.Result = _mapper.Map<List<OrderDetailDTO>>(OrderDetailList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        //[HttpGet("{id:int}", Name = "GetOrderDetail")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<APIResponse>> GetOrderDetail(int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //        {
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            return BadRequest(_response);
        //        }
        //        OrderHeaderDTO orderHeaderDTO = new OrderHeaderDTO();
        //        List<OrderDetailDTO> listOrderDetailDTO = new List<OrderDetailDTO>();

        //        OrderHeader orderHeader = await _dbOrderHeader.GetAsync(u => u.Id == id, includeProperties: "ApplicationUser");

        //        orderHeaderDTO = _mapper.Map<OrderHeaderDTO>(orderHeader);
        //        List<OrderDetail> listOrderDetail = new List<OrderDetail>();
        //        listOrderDetail = await _dbOrderDetail.GetAllAsync(u => u.OrderHeaderId == id, includeProperties: "Product");

        //        listOrderDetailDTO = _mapper.Map<List<OrderDetailDTO>>(listOrderDetail);

        //        OrderDetailDTO orderDetailDTO = new OrderDetailDTO();

        //        if (orderHeaderDTO == null)
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            return NotFound(_response);
        //        }
        //        orderDetailDTO.OrderHeaderDTO = orderHeaderDTO;
        //        orderDetailDTO.OrderDetailDTOList = listOrderDetailDTO;
        //        _response.Result = orderDetailDTO;
        //        _response.StatusCode = HttpStatusCode.OK;
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}


        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> CreateProduct([FromBody] ProductCreateDTO createDTO)
        //{
        //    try
        //    {

        //        if (await _dbProduct.GetAsync(u => u.Id == createDTO.Id) != null)
        //        {
        //            ModelState.AddModelError("ErrorMessages", "Product already Exists!");
        //            return BadRequest(ModelState);
        //        }
        //        if (await _dbCategory.GetAsync(u => u.Id == createDTO.CategoryId) == null)
        //        {
        //            ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
        //            return BadRequest(ModelState);
        //        }
        //        if (createDTO == null)
        //        {
        //            return BadRequest(createDTO);
        //        }

        //        Product product = _mapper.Map<Product>(createDTO);


        //        await _dbProduct.CreateAsync(product);
        //        _response.Result = _mapper.Map<ProductDTO>(product);
        //        _response.StatusCode = HttpStatusCode.Created;
        //        return CreatedAtRoute("GetProduct", new { id = product.Id }, _response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}


        //[Authorize(Roles = "admin")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[HttpDelete("{id:int}", Name = "DeleteProduct")]
        //public async Task<ActionResult<APIResponse>> DeleteProduct(int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //        {
        //            return BadRequest();
        //        }
        //        var product = await _dbProduct.GetAsync(u => u.Id == id);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }
        //        await _dbProduct.RemoveAsync(product);
        //        _response.StatusCode = HttpStatusCode.NoContent;
        //        _response.IsSuccess = true;
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}


        //[Authorize(Roles = "admin")]
        //[HttpPut("{id:int}", Name = "UpdateProduct")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<APIResponse>> UpdateProduct(int id, [FromBody] ProductUpdateDTO updateDTO)
        //{
        //    try
        //    {
        //        if (updateDTO == null || id != updateDTO.Id)
        //        {
        //            return BadRequest();
        //        }
        //        if (await _dbCategory.GetAsync(u => u.Id == updateDTO.CategoryId) == null)
        //        {
        //            ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
        //            return BadRequest(ModelState);
        //        }
        //        Product model = _mapper.Map<Product>(updateDTO);

        //        await _dbProduct.UpdateAsync(model);
        //        _response.StatusCode = HttpStatusCode.NoContent;
        //        _response.IsSuccess = true;
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}


        //[HttpPatch("{id:int}", Name = "UpdatePartialProduct")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UpdatePartialProduct(int id, JsonPatchDocument<ProductUpdateDTO> patchDTO)
        //{
        //    if (patchDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var product = await _dbProduct.GetAsync(u => u.Id == id, tracked: false);

        //    ProductUpdateDTO productDTO = _mapper.Map<ProductUpdateDTO>(product);


        //    if (product == null)
        //    {
        //        return BadRequest();
        //    }
        //    patchDTO.ApplyTo(productDTO, ModelState);
        //    Product model = _mapper.Map<Product>(productDTO);

        //    await _dbProduct.UpdateAsync(model);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return NoContent();
        //}


    }
}
