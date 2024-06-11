using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_Vehical_Api.DTOs;
using User_Vehical_Api.Models;
using User_Vehical_Api.Services.Contracts;
using User_Vehical_Api.Services.Implementation;

namespace User_Vehical_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {


        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        [HttpGet("Get All Vehicles Details")]
        public async Task<ActionResult<IEnumerable<DemoVehicle>>> GetAllUsers()
        {
            var vehicles = await _vehicleRepository.GetAllUsersAsync();
            return Ok(vehicles);
        }
        [HttpGet("Get Vehicle by pagination")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles([FromQuery] string search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (vehicles, totalCount) = await _vehicleRepository.GetVehiclesAsync(search, page, pageSize);

            Response.Headers.Add("X-Total-Count", totalCount.ToString());

            return Ok(vehicles);
        }
        [HttpPost("Add Vehicles")]

        public async Task<ActionResult> CreateVehicle(List<DemoVehicle> user)
        {
            await _vehicleRepository.AddVehicleAsync(user);
            return Ok("Data inserted Successfully");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicle(int id, DemoVehicle vehicle)
        {
            if (id != vehicle.VehicleNumber)
            {
                return BadRequest();
            }

            try
            {
                await _vehicleRepository.UpdateVehicleAsync(vehicle);
                return Ok("Data updated Successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

           
        }
    }
}
