using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook_API.Models.Dto
{
    public class ShoppingCartCreateDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

        public string ApplicationUserId { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
