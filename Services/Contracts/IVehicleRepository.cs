using User_Vehical_Api.DTOs;
using User_Vehical_Api.Models;

namespace User_Vehical_Api.Services.Contracts
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<DemoVehicle>> GetAllUsersAsync();
        Task AddVehicleAsync(List<DemoVehicle> vehicle);
        Task UpdateVehicleAsync(DemoVehicle vehicle);

        Task<(IEnumerable<Vehicle> Vehicles, int TotalCount)> GetVehiclesAsync(string search, int page, int pageSize);

    }
}
