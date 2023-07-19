namespace BulkyBook_API.Models
{
    public class UserDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
		public string? StreetAddress { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public string? PostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
