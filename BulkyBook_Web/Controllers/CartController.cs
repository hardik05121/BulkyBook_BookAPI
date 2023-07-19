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
using System.Data;
using System.Reflection;
using System.Security.Claims;

namespace BulkyBook_Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _applicationUserService;
        public CartController(IShoppingCartService shoppingCartService, IMapper mapper, 
            IProductService productService,IApplicationUserService applicationUserService)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
            _productService = productService;
            _applicationUserService = applicationUserService;
        }

        public async Task<IActionResult> IndexCart()
        {
            //Method:- 3

            List<ShoppingCartDTO> list = new();

            var response = await _shoppingCartService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ShoppingCartDTO>>(Convert.ToString(response.Result));
                ShoppingCartVM shoppingCartVM = new ShoppingCartVM
                {
                    ShoppingCartList = list
                };
                foreach (var cart in shoppingCartVM.ShoppingCartList)
                {
                   // cart.Price = GetPriceBasedOnQuantity(cart);
                    shoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
                }
                return View(shoppingCartVM);
            }
            return NotFound();
        }

        // this methos are used in the show the index in the shoppingcart model.
        //method:- 1
        //List<ShoppingCartDTO> list = new();

        //var response = await _shoppingCartService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        //if (response != null && response.IsSuccess)
        //{
        //    list = JsonConvert.DeserializeObject<List<ShoppingCartDTO>>(Convert.ToString(response.Result));
        //}
        //return View(list);

        //Method:-2

        // Create an empty list of ShoppingCartVM
        //List<ShoppingCartVM> list = new List<ShoppingCartVM>();

        //// Send a request to the API to get the shopping cart data
        //var response = await _shoppingCartService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));

        //if (response != null && response.IsSuccess)
        //{
        //    ShoppingCartDTO shoppingCart = JsonConvert.DeserializeObject<ShoppingCartDTO>(Convert.ToString(response.Result));

        //    ShoppingCartVM cart = new ShoppingCartVM
        //    {
        //        ShoppingCart = shoppingCart
        //    };

        //// Make changes to the cart data here (modify properties, add/remove items, etc.)

        //// Send the modified cart data back to the API
        //var modifiedResponse = await _shoppingCartService.ModifyCartAsync<APIResponse>(cart, HttpContext.Session.GetString(SD.SessionToken));

        //if (modifiedResponse != null && modifiedResponse.IsSuccess)
        //{
        //    ShoppingCartDTO modifiedCart = JsonConvert.DeserializeObject<ShoppingCartDTO>(Convert.ToString(modifiedResponse.Result))
        //    // Update the shopping cart data in the ViewModel
        //    cart.ShoppingCart = modifiedCart;
        //    return View(cart);
        //}
        //return View(cart);
        //}

        //return NotFound();


        public async Task<IActionResult> Summary()
        {
            //Method :-1
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ApplicationName = claimsIdentity.FindFirst(ClaimTypes.Name).Value;
            string Id = userId;

            ApplicationUserDTO applicationUserDTO = new ApplicationUserDTO();

            var responce = await _applicationUserService.GetAsync<APIResponse>(Id, HttpContext.Session.GetString(SD.SessionToken));
            if (responce != null && responce.IsSuccess)
            {
                applicationUserDTO = JsonConvert.DeserializeObject<ApplicationUserDTO>(Convert.ToString(responce.Result));

            }

            List<ShoppingCartDTO> list = new();
            var response = await _shoppingCartService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ShoppingCartDTO>>(Convert.ToString(response.Result));

                ShoppingCartVM shoppingCartVM = new ShoppingCartVM();

                shoppingCartVM.ShoppingCartList = list;
                shoppingCartVM.OrderHeader.ApplicationUser = applicationUserDTO;
                shoppingCartVM.OrderHeader.ApplicationUser.Id = applicationUserDTO.Id;
                shoppingCartVM.OrderHeader.ApplicationUser = shoppingCartVM.OrderHeader.ApplicationUser;
                shoppingCartVM.OrderHeader.Name = shoppingCartVM.OrderHeader.ApplicationUser.Name;
                shoppingCartVM.OrderHeader.PhoneNumber = shoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
                shoppingCartVM.OrderHeader.StreetAddress = shoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
                shoppingCartVM.OrderHeader.City = shoppingCartVM.OrderHeader.ApplicationUser.City;
                shoppingCartVM.OrderHeader.State = shoppingCartVM.OrderHeader.ApplicationUser.State;
                shoppingCartVM.OrderHeader.PostalCode = shoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

                foreach (var cart in shoppingCartVM.ShoppingCartList)
                {
                    // cart.Price = GetPriceBasedOnQuantity(cart);
                    shoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
                }
                return View(shoppingCartVM);
            }
            return NotFound();
        }

        //Method:-2
        //             var claimsIdentity = (ClaimsIdentity)User.Identity;
        //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        //var ApplicationName = claimsIdentity.FindFirst(ClaimTypes.Name).Value;
        //string Id = userId;


        //         ApplicationUserDTO applicationUserDTO = new ApplicationUserDTO();

        //         var responce = await _applicationUserService.GetAsync<APIResponse>(Id, HttpContext.Session.GetString(SD.SessionToken));
        //         if (responce != null && responce.IsSuccess)
        //         {
        //            applicationUserDTO   = JsonConvert.DeserializeObject<ApplicationUserDTO>(Convert.ToString(responce.Result));
        //         }

        //         List<ShoppingCartDTO> list = new();
        //         var response = await _shoppingCartService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        //         if (response != null && response.IsSuccess)
        //         {
        //             list = JsonConvert.DeserializeObject<List<ShoppingCartDTO>>(Convert.ToString(response.Result));
        //             ShoppingCartVM shoppingCartVM = new ShoppingCartVM()
        //             {
        //                 ApplicationUser = applicationUserDTO,
        //                 ShoppingCartList = list
        //             };
        //             shoppingCartVM.OrderHeader.ApplicationUser = shoppingCartVM.ApplicationUser;
        //             shoppingCartVM.OrderHeader.PhoneNumber = shoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
        //             shoppingCartVM.OrderHeader.StreetAddress = shoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
        //             shoppingCartVM.OrderHeader.City = shoppingCartVM.OrderHeader.ApplicationUser.City;
        //             shoppingCartVM.OrderHeader.State = shoppingCartVM.OrderHeader.ApplicationUser.State;
        //             shoppingCartVM.OrderHeader.PostalCode = shoppingCartVM.OrderHeader.ApplicationUser.PostalCode;


        //             foreach (var cart in shoppingCartVM.ShoppingCartList)
        //             {

        //                 // cart.Price = GetPriceBasedOnQuantity(cart);
        //                 shoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
        //             }
        //             return View(shoppingCartVM);
        //         }
        //         return NotFound();

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SummaryPOST()
        {
            // 
            //shoppingCartVM.ShoppingCart.Count = Count;
            if (ModelState.IsValid)
            {
                var response = await _shoppingCartService.SummaryPOST<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "SummaryPost created successfully";
                    return RedirectToAction(nameof(IndexCart));
                }
            }
            TempData["error"] = "Error encountered.";
            return View();
        }


        public async Task<IActionResult> Plus(int Id,int Count,int ProductId)
        {
            ShoppingCartUpdateDTO shoppingCartUpdateDTO= new ShoppingCartUpdateDTO();
            shoppingCartUpdateDTO.Id = Id;  
            shoppingCartUpdateDTO.Count = Count + 1;
            shoppingCartUpdateDTO.ProductId = ProductId;
            var response = await _shoppingCartService.UpdateAsync<APIResponse>(shoppingCartUpdateDTO, HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Data updated sucessful to cart.";
                return RedirectToAction("IndexCart");
            }
            return NotFound();
           
        }

        public async Task<IActionResult> Minus(int Id, int Count, int ProductId)
        {
            if(Count <= 1)
            {
                var response = await _shoppingCartService.DeleteAsync<APIResponse>(Id, HttpContext.Session.GetString(SD.SessionToken));

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Data deleted sucessful to cart.";
                    return RedirectToAction("IndexCart");
                }
            }
            else
            {
                ShoppingCartUpdateDTO shoppingCartUpdateDTO = new ShoppingCartUpdateDTO();
                shoppingCartUpdateDTO.Id = Id;
                shoppingCartUpdateDTO.Count = Count - 1;
                shoppingCartUpdateDTO.ProductId = ProductId;
                var response = await _shoppingCartService.UpdateAsync<APIResponse>(shoppingCartUpdateDTO, HttpContext.Session.GetString(SD.SessionToken));

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Data updated sucessful to cart.";
                    return RedirectToAction("IndexCart");
                }
            }
           // return NotFound();
            return NotFound(string.Empty);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _shoppingCartService.DeleteAsync<APIResponse>(Id, HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsSuccess)
            {
                TempData["sucess"] = "Data Deleted sucessful to cart.";
                return RedirectToAction("IndexCart");
            }
            return NotFound();

        }

        //private double GetPriceBasedOnQuantity(ShoppingCartDTO shoppingCart)
        //{
        //    if (shoppingCart.Count <= 50)
        //    {
        //        return shoppingCart.Product.Price;
        //    }
        //    else
        //    {
        //        if (shoppingCart.Count <= 100)
        //        {
        //            return shoppingCart.Product.Price50;
        //        }
        //        else
        //        {
        //            return shoppingCart.Product.Price100;
        //        }
        //    }
        //}

     
    }
}

