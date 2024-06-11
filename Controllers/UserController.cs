using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_Vehical_Api.DTOs;
using User_Vehical_Api.Models;
using User_Vehical_Api.Services.Contracts;

namespace User_Vehical_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("Get All Users")]
        public async Task<ActionResult<IEnumerable<DemoUser>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpPost("Add Users")]

        public async Task<ActionResult> CreateUser(List<DemoUser> user)
        {
            await _userRepository.AddUserAsync(user);
            return Ok("Data inserted Successfully");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, DemoUser userDemo)
        {
            if (id != userDemo.UserId)
            {
                return BadRequest();
            }

            try
            {
                await _userRepository.UpdateUserAsync(userDemo);
                return Ok("Data updated Successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

           
        }

    }
}
