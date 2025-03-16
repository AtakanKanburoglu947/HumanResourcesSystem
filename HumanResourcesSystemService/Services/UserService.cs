using AutoMapper;
using HumanResourcesSystemCore;
using HumanResourcesSystemCore.Dtos;
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

        public async Task Remove(string id)
        {
            _userRepository.Remove(id);
            await _unitOfWork.CommitAsync();
        }
        public async Task Update(UserDto userDto)
        {

            _userRepository.Update(_mapper.Map<User>(userDto));
            await _unitOfWork.CommitAsync();
        }
    }
}
