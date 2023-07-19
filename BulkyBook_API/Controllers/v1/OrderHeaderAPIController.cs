using AutoMapper;
using BulkyBook_API.Models;
using BulkyBook_API.Models.Dto;
using BulkyBook_API.Models.VM;
using BulkyBook_API.Repository.IRepostiory;
using BulkyBook_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Net;

namespace BulkyBook_API.Controllers.v1
{

    [Route("api/v{version:apiVersion}/[Controller]/[Action]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class OrderHeaderAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IOrderDetailRepository _dbOrderDetail;
        private readonly IOrderHeaderRepository _dbOrderHeader;
        private readonly IProductRepository _dbProduct;
        private readonly IMapper _mapper;
        public OrderHeaderAPIController(IOrderDetailRepository dbOrderDetail,
            IMapper mapper, IProductRepository dbProduct,
            IOrderHeaderRepository dbOrderHeader)
        {

            _mapper = mapper;
            _response = new();
            _dbOrderDetail = dbOrderDetail;
            _dbOrderHeader = dbOrderHeader;
            _dbProduct = dbProduct;
        }


        [HttpGet(Name = "GetOrderHeaders")]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetOrderHeaders()
        {
            try
            {
                IEnumerable<OrderHeader> OrderHeaderList = await _dbOrderHeader.
                    GetAllAsync(includeProperties: "ApplicationUser");

                _response.Result = _mapper.Map<List<OrderHeaderDTO>>(OrderHeaderList);
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

        [HttpGet("{id:int}", Name = "GetOrderHeader")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetOrderHeader(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var OrderHeader = await _dbOrderHeader.
                    GetAsync(u => u.Id == id, includeProperties: "ApplicationUser");

                if (OrderHeader == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<OrderHeaderDTO>(OrderHeader);
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


        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateOrderHeader")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateOrderHeader(int id, [FromBody] OrderVM orderVM)
        {
            try
            {
                OrderHeaderDTO orderHeaderDTO = orderVM.OrderHeader;
                OrderHeader orderHeader = _mapper.Map<OrderHeader>(orderHeaderDTO);

              
                OrderHeader orderHeaderFromDb = await _dbOrderHeader.GetAsync(u => u.Id == orderHeader.Id, includeProperties: "ApplicationUser");

                orderHeaderFromDb.Name = orderHeader.Name;
                orderHeaderFromDb.PhoneNumber = orderHeader.PhoneNumber;
                orderHeaderFromDb.StreetAddress = orderHeader.StreetAddress;
                orderHeaderFromDb.City = orderHeader.City;
                orderHeaderFromDb.State = orderHeader.State;
                orderHeaderFromDb.PostalCode = orderHeader.PostalCode;

                if (!string.IsNullOrEmpty(orderHeader.Carrier))
                {
                    orderHeaderFromDb.Carrier = orderHeader.Carrier;
                }
                if (!string.IsNullOrEmpty(orderHeader.TrackingNumber))
                {
                    orderHeaderFromDb.Carrier = orderHeader.TrackingNumber;
                }

                await _dbOrderHeader.UpdateAsync(orderHeaderFromDb);
                _response.Result = _mapper.Map<OrderHeaderDTO>(orderHeaderFromDb);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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


        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "StartProcessing")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> StartProcessing(int id, [FromBody] OrderVM orderVM)
        {
            try
            {
                OrderHeaderDTO orderHeaderDTO = orderVM.OrderHeader;
                OrderHeader orderHeader = _mapper.Map<OrderHeader>(orderHeaderDTO);

              
                OrderHeader orderHeaderFromDb = await _dbOrderHeader.GetAsync(u => u.Id == orderHeader.Id, includeProperties: "ApplicationUser");

                orderHeaderFromDb.OrderStatus = SD.StatusInProcess;

                await _dbOrderHeader.UpdateAsync(orderHeaderFromDb);
                _response.Result = _mapper.Map<OrderHeaderDTO>(orderHeaderFromDb);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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


        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "ShipOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> ShipOrder(int id, [FromBody] OrderVM orderVM)
        {
            try
            {
                OrderHeaderDTO orderHeaderDTO = orderVM.OrderHeader;
                OrderHeader orderHeader = _mapper.Map<OrderHeader>(orderHeaderDTO);

              
                OrderHeader orderHeaderFromDb = await _dbOrderHeader.GetAsync(u => u.Id == orderHeader.Id, includeProperties: "ApplicationUser");

                orderHeaderFromDb.TrackingNumber=orderHeader.TrackingNumber;
                orderHeaderFromDb.Carrier = orderHeader.Carrier;
                orderHeaderFromDb.OrderStatus = SD.StatusShipped;
                orderHeaderFromDb.ShippingDate = DateTime.Now;

                if (orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment)
                {
                    orderHeaderFromDb.PaymentDueDate = DateTime.Now.AddDays(30);
                }


                await _dbOrderHeader.UpdateAsync(orderHeaderFromDb);
                _response.Result = _mapper.Map<OrderHeaderDTO>(orderHeaderFromDb);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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
        
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "CancelOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CancelOrder(int id, [FromBody] OrderVM orderVM)
        {
            try
            {
                OrderHeaderDTO orderHeaderDTO = orderVM.OrderHeader;
                OrderHeader orderHeader = _mapper.Map<OrderHeader>(orderHeaderDTO);

              
                OrderHeader orderHeaderFromDb = await _dbOrderHeader.GetAsync(u => u.Id == orderHeader.Id, includeProperties: "ApplicationUser");


                //if (orderHeader.PaymentStatus == SD.PaymentStatusApproved)
                //{
                //    var options = new RefundCreateOptions
                //    {
                //        Reason = RefundReasons.RequestedByCustomer,
                //        PaymentIntent = orderHeader.PaymentIntentId
                //    };

                //    var service = new RefundService();
                //    Refund refund = service.Create(options);

                //    orderHeaderFromDb.OrderStatus = SD.StatusCancelled;
                //    orderHeaderFromDb.PaymentStatus = SD.StatusRefunded;

                //  //  _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusRefunded);
                //}
                //else
                //{
                //    orderHeaderFromDb.OrderStatus = SD.StatusCancelled;
                //    orderHeaderFromDb.PaymentStatus = SD.StatusCancelled;
                //   // _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusCancelled);
                //}

                if (orderHeader.PaymentStatus == SD.PaymentStatusApproved)
                {
                    var options = new RefundCreateOptions
                    {
                        Reason = RefundReasons.RequestedByCustomer,
                        PaymentIntent = orderHeader.PaymentIntentId
                    };

                    var service = new RefundService();
                    //Refund refund = service.Create(options);

                    orderHeaderFromDb.OrderStatus = SD.StatusCancelled;
                    orderHeaderFromDb.OrderStatus = SD.StatusRefunded;

                    //_dbOrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusRefunded);
                }
                else
                {
                    orderHeaderFromDb.OrderStatus = SD.StatusCancelled;
                    orderHeaderFromDb.OrderStatus = SD.StatusCancelled;
                    // _dbOrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusCancelled);
                }

                await _dbOrderHeader.UpdateAsync(orderHeaderFromDb);
                _response.Result = _mapper.Map<OrderHeaderDTO>(orderHeaderFromDb);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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
