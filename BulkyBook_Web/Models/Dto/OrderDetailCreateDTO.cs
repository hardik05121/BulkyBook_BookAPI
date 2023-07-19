﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook_Web.Models.Dto
{
    public class OrderDetailCreateDTO
    {
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [Required]
        public int ProductId { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}
