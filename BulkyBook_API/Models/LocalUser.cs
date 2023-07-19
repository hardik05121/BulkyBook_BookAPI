using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook_API.Models
{
    public class LocalUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        //try this method
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        //public int? CompanyId { get; set; }
        //[ForeignKey("CompanyId")]
        //[ValidateNever]
        //public Company? Company { get; set; }
        //[NotMapped]
        //public string Role { get; set; }
    }
}
