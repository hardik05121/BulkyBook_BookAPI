﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BulkyBook_API.Models
{
    public class ApplicationUser : IdentityUser
    {
       
       // public string Id { get; set; }
        public string Name { get; set; }
		public string? StreetAddress { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public string? PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        ////public int? CompanyId { get; set; }
        ////[ForeignKey("CompanyId")]
        ////[ValidateNever]
        ////public Company? Company { get; set; }
        //[NotMapped]
        //public string Role { get; set; }
    }

}
