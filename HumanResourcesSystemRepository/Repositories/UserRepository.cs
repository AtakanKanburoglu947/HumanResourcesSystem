using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemRepository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(User user)
        {
            await _appDbContext.Users.AddAsync(user);
           
        }

        public async Task<User> FindAsync(string id)
        {
            User? user = await _appDbContext.Users.FindAsync(id);
            if (user != null)
            {
                return user;
            }
            return null!;
        }

        public async void Remove(string id)
        {
            var userInDatabase = await FindAsync(id);
            bool userExists = userInDatabase != null;
            if (userExists)
            {
                _appDbContext.Users.Remove(userInDatabase);
            }
        }

        public async void Update(User user)
        {
            var userInDatabase = await FindAsync(user.Id);
            bool userExists = userInDatabase != null;
            if (userExists)
            {
                _appDbContext.Users.Update(user);
            }
        }
    }
}
