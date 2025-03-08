using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Services
{
    public interface IService<T,Dto> where T : class where Dto : class
    {
        Task<T> FindAsync(string id);
        Task AddAsync(Dto dto);
        Task UpdateAsync(Dto dto);
        Task RemoveAsync(string id);
    }
}
