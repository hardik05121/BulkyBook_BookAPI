using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook_Web.Models.Dto
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }
        [ValidateNever]
        public OrderHeaderDTO OrderHeader { get; set; }


        [Required]
        public int ProductId { get; set; }
        [ValidateNever]
        public ProductDTO Product { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}
