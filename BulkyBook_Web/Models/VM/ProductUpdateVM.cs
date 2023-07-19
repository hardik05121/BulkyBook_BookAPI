﻿
using BulkyBook_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook_Web.Models.VM
{
    public class ProductUpdateVM
    {
        public ProductUpdateVM()
        {
            Product = new ProductUpdateDTO();
        }
        public ProductUpdateDTO Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}

