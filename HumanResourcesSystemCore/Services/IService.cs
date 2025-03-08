using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Services
{
    public interface IService<T> where T : class
    {
        Task<T> FindAsync(string id);
        Task<T> FindByNameAsync(string name);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> RemoveAsync(string id);
    }
}
