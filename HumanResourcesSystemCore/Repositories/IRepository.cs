using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Find(string id);
        Task<T> FindByName(string name);
        Task Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
