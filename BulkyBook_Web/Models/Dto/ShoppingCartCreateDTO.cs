using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook_Web.Models.Dto
{
    public class ShoppingCartCreateDTO
    {
        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}
