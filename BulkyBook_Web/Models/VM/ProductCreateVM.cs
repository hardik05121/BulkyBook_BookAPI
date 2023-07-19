using BulkyBook_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook_Web.Models.VM
{
    public class ProductCreateVM
    {
        public ProductCreateVM()
        {
            Product = new ProductCreateDTO();
        }
        public ProductCreateDTO Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
