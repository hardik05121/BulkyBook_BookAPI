using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook_API.Models.Dto
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }
        [ValidateNever]
        public OrderHeaderDTO OrderHeaderDTO { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }

        public List<OrderDetailDTO> OrderDetailDTOList { get; set; }
    }
}
