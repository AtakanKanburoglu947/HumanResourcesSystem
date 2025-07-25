﻿using HumanResourcesSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindAsync(string id);
        Task AddAsync(T entity);
        List<T> Where(Expression<Func<T, DateTime>> orderBy, Expression<Func<T, bool>> expression);
        List<T> Where(Expression<Func<T, bool>> expression);
        Task Remove(string id);
        void Update(T entity);
        List<T> GetAll();
        Task<T>? FirstOrDefault(Expression<Func<T, bool>> expression);
        List<T> Pagination(int startIndex, Expression<Func<T, bool>> whereExpression);

    }
}
