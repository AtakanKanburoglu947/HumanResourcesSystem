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
        Task Update(User user);
        Task Remove(string id);
        Task<User>? FirstOrDefault(Expression<Func<User, bool>> expression);
        List<User> Where(Expression<Func<User, bool>> expression);
        List<User> GetAll();
        List<User> Pagination(int startIndex, Expression<Func<User, bool>> whereExpression);
    }
}
