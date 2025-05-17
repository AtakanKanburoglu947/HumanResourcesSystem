using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemRepository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(User user)
        {
            await _appDbContext.Users.AddAsync(user);
           
        }

        public async Task<User> FindAsync(string id)
        {
            User? user = await _appDbContext.Users.FindAsync(id);
            if (user != null)
            {
                return user;
            }
            return null!;
        }

        public async Task<User>? FirstOrDefault(Expression<Func<User, bool>> expression)
        {
            User? user = await _appDbContext.Users.FirstOrDefaultAsync(expression);
            if (user != null)
            {
                return user;
            }
            return null!;
        }

        public List<User> GetAll()
        {
            return _appDbContext.Users.ToList();
        }

        public List<User> Pagination(int startIndex, Expression<Func<User, bool>> whereExpression)
        {
            if (whereExpression == null)
            {
                return _appDbContext.Users.Skip(startIndex).ToList();
            }
            return _appDbContext.Users.Where(whereExpression).Skip(startIndex).ToList();
        }

        public async Task Remove(string id)
        {
            var userInDatabase = await FindAsync(id);
            bool userExists = userInDatabase != null;
            if (userExists)
            {
                _appDbContext.Users.Remove(userInDatabase);
            }
        }

        public async Task Update(User user)
        {
            var userInDatabase = await _appDbContext.Users.FindAsync(user.Id);
            if (userInDatabase == null)
                throw new Exception("Kullanıcı bulunamadı.");

            // Alanları güncelle
            userInDatabase.FirstName = user.FirstName;
            userInDatabase.LastName = user.LastName;
            userInDatabase.BirthDate = user.BirthDate;
            userInDatabase.HireDate = user.HireDate;
            userInDatabase.ManagerId = user.ManagerId;
            userInDatabase.CompanyId = user.CompanyId;
            userInDatabase.DepartmentId = user.DepartmentId;

        }

        public List<User> Where(Expression<Func<User, bool>> expression)
        {
            return _appDbContext.Users.Where(expression).ToList();
        }
    }
}
