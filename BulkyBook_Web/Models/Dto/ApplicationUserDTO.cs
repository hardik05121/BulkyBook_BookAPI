﻿namespace BulkyBook_Web.Models.Dto
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
		public string? StreetAddress { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public string? PostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
