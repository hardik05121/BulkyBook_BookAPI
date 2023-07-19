using BulkyBook_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook_Web.Models.VM
{
    public class ShoppingCartVM
    {
        public ShoppingCartVM()
        {
            ShoppingCart = new ShoppingCartDTO();
            OrderHeader = new OrderHeaderDTO();
            ApplicationUser = new ApplicationUserDTO();
        }
        public ShoppingCartDTO ShoppingCart { get; set; }
        [ValidateNever]
        public List<ShoppingCartDTO> ShoppingCartList { get; set; } 

        public OrderHeaderDTO OrderHeader{ get; set; } 
        public ApplicationUserDTO ApplicationUser { get; set; }
    }
}
