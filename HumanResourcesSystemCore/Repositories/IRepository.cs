using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindAsync(string id);
        Task AddAsync(T entity);
        void Remove(string id);
        void Update(T entity);
    }
}
