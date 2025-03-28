using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Services
{
    public interface IUserService
    {
        Task<User>? FindAsync(string id);
        Task AddAsync(UserDto userDto);
        Task Update(UserDto userDto);
        Task Remove(string id);
        Task<User>? FirstOrDefault(Expression<Func<User, bool>> expression);

    }
}
