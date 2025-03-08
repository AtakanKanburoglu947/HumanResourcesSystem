using AutoMapper;
using HumanResourcesSystemCore;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemCore.Services;
using HumanResourcesSystemRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemService.Services
{
    public class Service<T, Dto> : IService<T, Dto> where T : class where Dto : class
    {
        private readonly IRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Service(IRepository<T> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(Dto dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<T> FindAsync(string id)
        {
            return await _repository.FindAsync(id);
        }
        public async Task RemoveAsync(string id)
        {
            _repository.Remove(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Dto dto)
        {
            _repository.Update(_mapper.Map<T>(dto));
            await _unitOfWork.CommitAsync();
        }
    }
}
