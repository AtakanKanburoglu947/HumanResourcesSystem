using HumanResourcesSystemCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemRepository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public async Task<T> FindAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            throw new Exception("Bulunamadı");
        }
        public async void Remove(string id)
        {
            var entity = await FindAsync(id);
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
