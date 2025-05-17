using HumanResourcesSystemCore.Models;
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
            return null;
        }

        public async Task<T>? FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            T? entity = await _dbSet.FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                return entity;
            }
            return null!;
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<T> Pagination(int startIndex, Expression<Func<T, bool>> whereExpression)
        {
            if (whereExpression == null)
            {
                return _dbSet.Skip(startIndex).Take(5).ToList();
            }
            return _dbSet.Where(whereExpression).Skip(startIndex).Take(5).ToList();
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

        public List<T> Where(Expression<Func<T, bool>> expression)
        {
            List<T> result = _dbSet.Where(expression).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
