using User_Vehical_Api.DTOs;
using User_Vehical_Api.Models;

namespace User_Vehical_Api.Services.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<DemoUser>> GetAllUsersAsync();
        Task AddUserAsync(List<DemoUser> user);
        Task UpdateUserAsync(DemoUser user);
    }
}
