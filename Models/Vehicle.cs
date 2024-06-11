using System.ComponentModel.DataAnnotations;

namespace User_Vehical_Api.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleNumber { get; set; }
        public string? VehicleType { get; set; }
        public int ChassisNumber { get; set; }
        public int EngineNumber { get; set; }
        public int ManufacturingYear { get; set; }
        public double LoadCarryingCapacity { get; set; }
        public string? MakeOfVehicle { get; set; }
        public int ModelNumber { get; set; }
        public string? BodyType { get; set; }
        public string? OrganizationName { get; set; }
        public int DeviceID { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
    }
}

