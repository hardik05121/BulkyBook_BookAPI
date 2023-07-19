﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook_Web.Models.Dto
{
    public class ShoppingCartDTO
    {

        [Required]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ValidateNever]
        public ProductDTO Product { get; set; }
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]

        public int Count { get; set; }

        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUserDTO ApplicationUser { get; set; }
	
		[NotMapped]
        public double Price { get; set; }
    }
}
