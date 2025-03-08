using HumanResourcesSystemCore;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<User>? FindAsync(string id)
        {
            return await _userRepository.FindAsync(id);
        }

        public async Task Remove(string id)
        {
            _userRepository.Remove(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(User user)
        {
            _userRepository.Update(user);
            await _unitOfWork.CommitAsync();
        }
    }
}
