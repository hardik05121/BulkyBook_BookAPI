
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook_API.Models.Dto
{
    public class ShoppingCartVM
    {
        public ShoppingCartVM()
        {
            ShoppingCart = new ShoppingCart();
            OrderHeader = new OrderHeader();
            ApplicationUser = new ApplicationUser();
        }
        public ShoppingCart ShoppingCart { get; set; }
        [ValidateNever]
        public List<ShoppingCart> ShoppingCartList { get; set; }

        public OrderHeader OrderHeader { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
