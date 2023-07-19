using BulkyBook_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook_Web.Models.VM
{
    public class OrderVM
    {
        public OrderVM()
        {
          //  OrderDetail = new OrderDetailDTO();
            OrderHeader = new OrderHeaderDTO();
        }

        [ValidateNever]
        public List<OrderDetailDTO> OrderDetailList { get; set; }
        public OrderHeaderDTO OrderHeader { get; set; }
       // public OrderDetailDTO OrderDetail { get; set; }
    }
}
