using System.ComponentModel.DataAnnotations;

namespace User_Vehical_Api.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? MobileNumber { get; set; }
        public string? Organization { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? Location { get; set;}
        public string? PhotoPath { get; set; }
        public ICollection<Vehicle>? Vehicles { get; set; }



    }
}
