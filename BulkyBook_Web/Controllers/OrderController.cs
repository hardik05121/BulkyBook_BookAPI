using AutoMapper;
using BulkyBook_Utility;
using BulkyBook_Web.Models;
using BulkyBook_Web.Models.Dto;
using BulkyBook_Web.Models.VM;
using BulkyBook_Web.Service;
using BulkyBook_Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace BulkyBook_Web.Controllers
{

    [Authorize]
    public class OrderController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IOrderHeaderService _orderHeaderService;
        private readonly IOrderDetailService _orderDetailService;

        public OrderController(IShoppingCartService shoppingCartService, IMapper mapper,
            IProductService productService, IApplicationUserService applicationUserService, 
            IOrderDetailService orderDetailService,IOrderHeaderService orderHeaderService)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
            _productService = productService;
            _applicationUserService = applicationUserService;
            _orderDetailService = orderDetailService;
            _orderHeaderService = orderHeaderService;
           
        }
        public async Task<IActionResult> Index()
        {
            List<OrderHeaderDTO> list = new();

            var response = await _orderHeaderService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderHeaderDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> Detail(int OrderId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            int id = OrderId;

            OrderHeaderDTO OrderHeader = new OrderHeaderDTO();
            var responce = await _orderHeaderService.GetAsync<APIResponse>(id,HttpContext.Session.GetString(SD.SessionToken));
           
            if (responce != null && responce.IsSuccess)
            {
                OrderHeader= JsonConvert.DeserializeObject<OrderHeaderDTO>(Convert.ToString(responce.Result));
            }

            List<OrderDetailDTO> list = new();
            var response = await _orderDetailService.GetAllAsync<APIResponse>(id,HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(Convert.ToString(response.Result));

                OrderVM orderVM = new OrderVM();
                orderVM.OrderDetailList = list;
                orderVM.OrderHeader = OrderHeader;

                return View(orderVM);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderDetail(OrderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderHeaderService.UpdateAsync<APIResponse>(orderVM,HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    OrderHeaderDTO orderHeaderDTO = JsonConvert.DeserializeObject<OrderHeaderDTO>(Convert.ToString(response.Result));

                    TempData["success"] = "Order header Detail Update successfully";
                    return RedirectToAction(nameof(Detail), new { OrderId = orderHeaderDTO.Id });
                }
            }
            TempData["error"] = "Error encountered.";
            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartProcessing(OrderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderHeaderService.StartProcessing<APIResponse>(orderVM, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    OrderHeaderDTO orderHeaderDTO = JsonConvert.DeserializeObject<OrderHeaderDTO>(Convert.ToString(response.Result));

                    TempData["success"] = "Order in process successfully";
                    return RedirectToAction(nameof(Detail), new { OrderId = orderHeaderDTO.Id });
                }
            }
            TempData["error"] = "Error encountered.";
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShipOrder(OrderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderHeaderService.ShipOrder<APIResponse>(orderVM, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    OrderHeaderDTO orderHeaderDTO = JsonConvert.DeserializeObject<OrderHeaderDTO>(Convert.ToString(response.Result));

                    TempData["success"] = "Order Shipped Successfully";
                    return RedirectToAction(nameof(Detail), new { OrderId = orderHeaderDTO.Id });
                }
            }
            TempData["error"] = "Error encountered.";
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelOrder(OrderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderHeaderService.CancelOrder<APIResponse>(orderVM, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    OrderHeaderDTO orderHeaderDTO = JsonConvert.DeserializeObject<OrderHeaderDTO>(Convert.ToString(response.Result));

                    TempData["success"] = "Order Cancel successfully";
                    return RedirectToAction(nameof(Detail), new { OrderId = orderHeaderDTO.Id });
                }
            }
            TempData["error"] = "Error encountered.";
            return View();
        }



    }
}
