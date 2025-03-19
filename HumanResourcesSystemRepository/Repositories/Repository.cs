using HumanResourcesSystemCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task Remove(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public List<T> Where(Expression<Func<T, DateTime>> orderBy, Expression<Func<T, bool>> expression)
        {
            List<T> result = _dbSet.OrderByDescending(orderBy).Where(expression).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
