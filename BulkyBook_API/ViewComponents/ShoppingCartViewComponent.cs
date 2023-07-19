//using BulkyBook_API.Models.Dto;
//using BulkyBook_API.Repository.IRepostiory;
//using BulkyBook_Utility;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace BulkyBook_API.ViewComponents
//{
//    public class ShoppingCartViewComponent : ViewComponent
//    {

//        private readonly IShoppingCartRepository _dbshoppingCart;
//        public ShoppingCartViewComponent(IShoppingCartRepository dbshoppingCart)
//        {
//            _dbshoppingCart = dbshoppingCart;
//        }

//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            var claimsIdentity = (ClaimsIdentity)User.Identity;
//            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
//            ShoppingCartVM shoppingCartVM = new ShoppingCartVM();
//            if (claim != null)
//            {

//                if (HttpContext.Session.GetInt32(SD.SessionCart) == null)
//                {
                    

//                    HttpContext.Session.SetInt32(SD.SessionCart,
//                   await _dbshoppingCart.GetAllAsync(u => u.ApplicationUserId == claim.Value).Count());
//                }

//                return View(HttpContext.Session.GetInt32(SD.SessionCart));
//            }
//            else
//            {
//                HttpContext.Session.Clear();
//                return View(0);
//            }
//        }

//    }
//}
