using AutoMapper;
using HumanResourcesSystemCore;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(UserDto userDto)
        {
            await _userRepository.AddAsync(_mapper.Map<User>(userDto));
            await _unitOfWork.CommitAsync();
        }

        public async Task<User>? FindAsync(string id)
        {
            return await _userRepository.FindAsync(id);
        }

        public async Task<User>? FirstOrDefault(Expression<Func<User, bool>> expression)
        {
            return await _userRepository.FirstOrDefault(expression);
        }

        public async Task Remove(string id)
        {
            _userRepository.Remove(id);
            await _unitOfWork.CommitAsync();
        }
        public async Task Update(User userDto)
        {

            await _userRepository.Update(userDto);
            await _unitOfWork.CommitAsync();
        }

        public List<User> Where(Expression<Func<User, bool>> expression)
        {
            return _userRepository.Where(expression);
        }
        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public List<User> Pagination(int startIndex, Expression<Func<User, bool>> whereExpression)
        {
            return _userRepository.Pagination(startIndex, whereExpression);
        }
    }
}
