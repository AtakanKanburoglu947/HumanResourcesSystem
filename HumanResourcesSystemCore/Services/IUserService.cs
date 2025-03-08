using HumanResourcesSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Services
{
    public interface IUserService
    {
        Task<User>? FindAsync(string id);
        Task AddAsync(User user);
        Task Update(User user);
        Task Remove(string id);
    }
}
