using AutoMapper;
using Azure;
using BulkyBook_API.Models;
using BulkyBook_API.Models.Dto;
using BulkyBook_API.Repository.IRepostiory;
using BulkyBook_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace BulkyBook_API.Controllers.v1
{
    //var claimsIdentity = (ClaimsIdentity)User.Identity;
    //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
    //string ApplicationUserId = userId;

    [Route("api/v{version:apiVersion}/[Controller]/[Action]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class CartAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IShoppingCartRepository _dbShoppingCart;
        private readonly IProductRepository _dbProduct;
        private readonly IApplicationUserRepository _dbApplicationUser;
        private readonly IOrderHeaderRepository _dbOrderHeader;
        private readonly IOrderDetailRepository _dbOrderDetail;

        private readonly IMapper _mapper;
        public CartAPIController(IShoppingCartRepository dbShoppingCart,IMapper mapper,IProductRepository dbProduct,
            IOrderHeaderRepository orderHeader,IOrderDetailRepository orderDetail,IApplicationUserRepository applicationUser)
        {
            _dbShoppingCart = dbShoppingCart;
            _mapper = mapper;
            _response = new();
            _dbProduct = dbProduct;
            _dbApplicationUser = applicationUser;
            _dbOrderHeader = orderHeader;
            _dbOrderDetail = orderDetail;
        }


        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "String1", "string2" };
        }

        [HttpGet(Name = "GetShoppingCarts")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetShoppingCarts()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ApplicationName = claimsIdentity.FindFirst(ClaimTypes.Name).Value;
			string ApplicationUserId = userId;
            string ApplicationUserIdName = ApplicationName;

			try
            {
                IEnumerable<ShoppingCart> shoppingCartList = await _dbShoppingCart.GetAllAsync(includeProperties: "Product,ApplicationUser");
                _response.Result = _mapper.Map<List<ShoppingCartDTO>>(shoppingCartList);
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


        [HttpGet("{id:int}", Name = "GetShoppingCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetShoppingCart(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var shoppingCart = await _dbProduct.GetAsync(u => u.Id == id, includeProperties: "ApplicationUser");
                if (shoppingCart == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ShoppingCartDTO>(shoppingCart);
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
        [HttpPost(Name = "CreateShoppingCart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateShoppingCart([FromBody] ShoppingCartCreateDTO createDTO)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                createDTO.ApplicationUserId = userId;

                ShoppingCart shoppingCart = new ShoppingCart();
                shoppingCart.ApplicationUserId = userId;    
                shoppingCart.ProductId = createDTO.ProductId;
                shoppingCart.Count = createDTO.Count;

                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                //if (await _dbShoppingCart.GetAsync(u => u.ProductId.ToString() == createDTO.ProductId.ToString()) != null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "cart already Exists!");
                //    return BadRequest(ModelState);
                //}

                //if (createDTO == null)
                //{
                //    return BadRequest(createDTO);
                //}

                //ShoppingCart shoppingCart = _mapper.Map<ShoppingCart>(createDTO);

                //if(shoppingCart != null)
                //{
                //    shoppingCart.Count += shoppingCart.Count;
                //    _dbShoppingCart.UpdateAsync(shoppingCart);
                //    _dbShoppingCart.SaveAsync();
                //}
                //else
                //{
                    
                //    _dbShoppingCart.CreateAsync(shoppingCart);
                //    _dbShoppingCart.SaveAsync();

                //}

                await _dbShoppingCart.CreateAsync(shoppingCart);
                _response.Result = _mapper.Map<ShoppingCartDTO>(shoppingCart);
                _response.StatusCode = HttpStatusCode.Created;
                return _response;

                //return CreatedAtRoute("GetShoppingCart", new { id = shoppingCart.ProductId }, _response);
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
        [HttpPut("{id:int}", Name = "UpdateShoppingCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateShoppingCart(int id, [FromBody] ShoppingCartUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                //if (await _dbCategory.GetAsync(u => u.Id == updateDTO.CategoryId,false) == null)
                //{
                //	ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
                //	return BadRequest(ModelState);
                //}
                ShoppingCart model = _mapper.Map<ShoppingCart>(updateDTO);
               // model.Count += 1;

                await _dbShoppingCart.UpdateAsync(model);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteShoppingCart")]
        public async Task<ActionResult<APIResponse>> DeleteShoppingCart(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    return BadRequest();
                }
                var shoppingCart = await _dbShoppingCart.GetAsync(u => u.Id == Id, tracked: true);
                if (shoppingCart == null)
                {
                    return NotFound();
                }
                await _dbShoppingCart.RemoveAsync(shoppingCart);
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

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPatch("{id:int}", Name = "UpdatePartialShoppingCart")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialShoppingCart(int id, JsonPatchDocument<ShoppingCartUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var shoppingCart = await _dbShoppingCart.GetAsync(u => u.Id == id, tracked: false);

            ShoppingCartUpdateDTO shoppingCartDTO = _mapper.Map<ShoppingCartUpdateDTO>(shoppingCart);


            if (shoppingCart == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(shoppingCartDTO, ModelState);
            ShoppingCart model = _mapper.Map<ShoppingCart>(shoppingCartDTO);

            await _dbShoppingCart.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        //[Route("/SummaryPOST")]
        //[ApiVersion("1.0")]
        [HttpPost(Name = "SummaryPOST")]
        //[ValidateAntiForgeryToken]
        //[ApiExplorerSettings(IgnoreApi = true)]

        public async Task<ActionResult<APIResponse>> SummaryPOST()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                ShoppingCartVM ShoppingCartVM = new ShoppingCartVM();


                ShoppingCartVM.ShoppingCartList = await _dbShoppingCart.GetAllAsync(u => u.ApplicationUserId == userId,
                    includeProperties: "Product");

            //    ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
                ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

                ApplicationUser applicationUser = await _dbApplicationUser.GetAsync(u => u.Id == userId);


                foreach (var cart in ShoppingCartVM.ShoppingCartList)
                {
                   
                
                    ShoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
                }
                ShoppingCartVM.OrderHeader.ApplicationUser = applicationUser;

                ShoppingCartVM.OrderHeader.ApplicationUser.Id = applicationUser.Id;

                ShoppingCartVM.OrderHeader.Name = applicationUser.Name;
                ShoppingCartVM.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
                ShoppingCartVM.OrderHeader.StreetAddress = applicationUser.StreetAddress;
                ShoppingCartVM.OrderHeader.City = applicationUser.City;
                ShoppingCartVM.OrderHeader.State = applicationUser.State;
                ShoppingCartVM.OrderHeader.PostalCode = applicationUser.PostalCode;

                //it is a regular customer 
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;

                await _dbOrderHeader.CreateAsync(ShoppingCartVM.OrderHeader);

                foreach (var cart in ShoppingCartVM.ShoppingCartList)
                {
                    OrderDetail orderDetail = new()
                    {
                        ProductId = cart.ProductId,
                        OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                        Price = cart.Product.Price,
                        Count = cart.Count
                    };
                    await _dbOrderDetail.CreateAsync(orderDetail);
                }

                //it is a regular customer account and we need to capture payment
                //stripe logic
                //var domain = Request.Scheme + "://" + Request.Host.Value + "/";
                //var options = new SessionCreateOptions
                //{
                //    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                //    CancelUrl = domain + "customer/cart/index",
                //    LineItems = new List<SessionLineItemOptions>(),
                //    Mode = "payment",
                //};

                //foreach (var item in ShoppingCartVM.ShoppingCartList)
                //{
                //    var sessionLineItem = new SessionLineItemOptions
                //    {
                //        PriceData = new SessionLineItemPriceDataOptions
                //        {
                //            UnitAmount = (long)(item.Price * 100), // $20.50 => 2050
                //            Currency = "usd",
                //            ProductData = new SessionLineItemPriceDataProductDataOptions
                //            {
                //                Name = item.Product.Title
                //            }
                //        },
                //        Quantity = item.Count
                //    };
                //    options.LineItems.Add(sessionLineItem);
                //}

                //var service = new SessionService();
                //Session session = service.Create(options);
                _dbOrderHeader.UpdateStripePaymentID(ShoppingCartVM.OrderHeader.Id, "1", "1");
                await _dbOrderHeader.SaveAsync();


                //Response.Headers.Add("Location", session.Url);
                //return new StatusCodeResult(303);
              
               // _response.Result = orderHeader.Id;
                _response.StatusCode = HttpStatusCode.OK;
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

            // return RedirectToAction(nameof(OrderConfirmation), new { id = ShoppingCartVM.OrderHeader.Id });
        }
    }

}
