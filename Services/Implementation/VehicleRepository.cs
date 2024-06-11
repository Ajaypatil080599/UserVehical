using Microsoft.EntityFrameworkCore;
using User_Vehical_Api.DTOs;
using User_Vehical_Api.Models;
using User_Vehical_Api.Services.Contracts;

namespace User_Vehical_Api.Services.Implementation
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _appDbContext;
         public VehicleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddVehicleAsync(List<DemoVehicle> vehicle)
        {
            List<Vehicle> livehicle = new List<Vehicle>();
            for (int i = 0; i<vehicle.Count; i++)
            {
                Vehicle us = new Vehicle();
                us.VehicleType = vehicle[i].VehicleType;
                us.ChassisNumber = vehicle[i].ChassisNumber;
                us.EngineNumber = vehicle[i].EngineNumber;
                us.ManufacturingYear = vehicle[i].ManufacturingYear;
                us.LoadCarryingCapacity = vehicle[i].LoadCarryingCapacity;
                us.MakeOfVehicle = vehicle[i].MakeOfVehicle;
                us.ModelNumber = vehicle[i].ModelNumber;
                us.BodyType = vehicle[i].BodyType;
                us.OrganizationName = vehicle[i].OrganizationName;
                us.DeviceID = vehicle[i].DeviceID;
                us.UserID = vehicle[i].UserID;
                livehicle.Add(us);
            }
            await _appDbContext.AddRangeAsync(livehicle);
            await _appDbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<DemoVehicle>> GetAllUsersAsync()
        {
            var users = from us in _appDbContext.Vehicles
                        select new DemoVehicle
                        {
                            VehicleNumber = us.VehicleNumber,
                            VehicleType=us.VehicleType,
                            ChassisNumber=us.ChassisNumber,
                            EngineNumber=us.EngineNumber,
                            ManufacturingYear=us.ManufacturingYear,
                            LoadCarryingCapacity=us.LoadCarryingCapacity,
                            MakeOfVehicle=us.MakeOfVehicle,
                            ModelNumber=us.ModelNumber,
                            BodyType=us.BodyType,
                            OrganizationName=us.OrganizationName,
                            DeviceID=us.DeviceID,
                            UserID=us.UserID
                        };
            return await users.ToListAsync();
        }

        public async Task<(IEnumerable<Vehicle> Vehicles, int TotalCount)> GetVehiclesAsync(string search, int page, int pageSize)
        {
            var query = _appDbContext.Vehicles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(v =>  v.VehicleType.Contains(search) ||
                                          v.MakeOfVehicle.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var vehicles = await query.Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

            return (vehicles, totalCount);
        }

        public async Task UpdateVehicleAsync(DemoVehicle vehicle)
        {
            var existingvehicle = await _appDbContext.Vehicles.FindAsync(vehicle.VehicleNumber);
            if (existingvehicle == null)
            {
                throw new KeyNotFoundException($"User with ID {vehicle.VehicleNumber} not found.");
            }

           existingvehicle.VehicleType=vehicle.VehicleType;
            existingvehicle.ChassisNumber = vehicle.ChassisNumber;
            existingvehicle.EngineNumber = vehicle.EngineNumber;
            existingvehicle.ManufacturingYear = vehicle.ManufacturingYear;
            existingvehicle.LoadCarryingCapacity=vehicle.LoadCarryingCapacity;
            existingvehicle.MakeOfVehicle = vehicle.MakeOfVehicle;
            existingvehicle.ModelNumber = vehicle.ModelNumber;
            existingvehicle.BodyType = vehicle.BodyType;
            existingvehicle.OrganizationName = vehicle.OrganizationName;
            existingvehicle.DeviceID = vehicle.DeviceID;
            existingvehicle.UserID=vehicle.UserID;

             _appDbContext.Vehicles.Update(existingvehicle);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
