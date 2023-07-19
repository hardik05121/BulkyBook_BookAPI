using AutoMapper;
using BulkyBook_Utility;
using BulkyBook_Web.Models;
using BulkyBook_Web.Models.Dto;
using BulkyBook_Web.Models.VM;
using BulkyBook_Web.Service;
using BulkyBook_Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBook_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IShoppingCartService _shoppingCartService;


        public HomeController(IProductService productService, IMapper mapper, IShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _mapper = mapper;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDTO> list = new();

            var response = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {

            //List<ShoppingCartDTO> cart = new();

            var response = await _productService.GetAsync<APIResponse>(Id, HttpContext.Session.GetString(SD.SessionToken));
            //ShoppingCartDTO cart = null;
            if (response != null && response.IsSuccess)
            {
                ProductDTO product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                ShoppingCartDTO cart = new ShoppingCartDTO
                {
                    ProductId = product.Id,
                    Product = product,
                    Count = 1
                };
                return View(cart);
            }

            return NotFound();
        }

        [Authorize(Roles ="admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Details(ShoppingCartDTO cartDto)
        {
            ShoppingCartCreateDTO cart = new ShoppingCartCreateDTO();
            cart.ProductId = cartDto.Id;
            cart.Count = cartDto.Count;

            var responce = await _shoppingCartService.CreateAsync<APIResponse>(cart, HttpContext.Session.GetString(SD.SessionToken));

            if(responce != null && responce.IsSuccess) 
            {
                TempData["success"] = "Product addd to the cart.";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

    }
}