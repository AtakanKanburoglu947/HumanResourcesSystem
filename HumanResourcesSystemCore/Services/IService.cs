﻿using HumanResourcesSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Services
{
    public interface IService<T,Dto> where T : class where Dto : class
    {
        Task<T> FindAsync(string id);
        Task AddAsync(Dto dto);
        Task UpdateAsync(T entity);
        Task RemoveAsync(string id);
        List<T> Where(Expression<Func<T, DateTime>> orderBy, Expression<Func<T, bool>> expression);
        List<T> Where(Expression<Func<T, bool>> expression);
        List<T> GetAll();
        Task<T>? FirstOrDefault(Expression<Func<T, bool>> expression);
        List<T> Pagination(int startIndex, Expression<Func<T, bool>> whereExpression);



    }
}
