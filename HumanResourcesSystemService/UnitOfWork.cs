using HumanResourcesSystemCore;
using HumanResourcesSystemRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemService
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task CommitAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
