using HumanResourcesSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Repositories
{
    public interface IUserRepository
    {
        Task<User>? FindAsync(string id);
        Task AddAsync(User user);
        void Update(User user);
        void Remove(string id);
        Task<User>? FirstOrDefault(Expression<Func<User, bool>> expression);

    }
}
