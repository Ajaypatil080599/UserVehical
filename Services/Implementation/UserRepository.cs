using Microsoft.EntityFrameworkCore;
using User_Vehical_Api.DTOs;
using User_Vehical_Api.Models;
using User_Vehical_Api.Services.Contracts;

namespace User_Vehical_Api.Services.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

       

        public async Task<IEnumerable<DemoUser>> GetAllUsersAsync()
        {
            var users = from us in _context.Users
                        select new DemoUser
                        {
                            UserId=us.UserId,
                            Name = us.Name,
                            MobileNumber=us.MobileNumber,
                            Organization=us.Organization,
                            Address=us.Address,
                            EmailAddress=us.EmailAddress,
                            Location=us.Location,
                            PhotoPath=us.PhotoPath
                        };
               return await users.ToListAsync();
            //return await _context.Users.Include(u => u.Vehicles).ToListAsync();
        }
        public async Task AddUserAsync(List<DemoUser> user)
        {
            List<User> liuser = new List<User>();
            for(int i=0;i<user.Count; i++)
            {
                User us = new User();
                us.Name = user[i].Name;
                us.MobileNumber = user[i].MobileNumber;
                us.Organization = user[i].Organization;
                us.Address = user[i].Address;
                us.EmailAddress = user[i].EmailAddress;
                us.Location = user[i].Location;
                us.PhotoPath = user[i].PhotoPath;
                liuser.Add(us);
            }
             await _context.AddRangeAsync(liuser);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(DemoUser user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {user.UserId} not found.");
            }

            // Update the existing user properties with values from the UserDemo instance
            existingUser.Name = user.Name;
            existingUser.MobileNumber = user.MobileNumber;
            existingUser.Organization = user.Organization;
            existingUser.Address = user.Address;
            existingUser.EmailAddress = user.EmailAddress;
            existingUser.Location = user.Location;
            existingUser.PhotoPath = user.PhotoPath;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

        }
    }
}
