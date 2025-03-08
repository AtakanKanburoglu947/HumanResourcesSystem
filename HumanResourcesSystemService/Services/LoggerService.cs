using HumanResourcesSystemCore;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemService.Services
{
    public class LoggerService : ILoggerService
    {
        private ILoggerRepository _loggerRepository;
        private IUnitOfWork _unitOfWork;
        public LoggerService(ILoggerRepository loggerRepository, IUnitOfWork unitOfWork) {
                   
            _loggerRepository = loggerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Log(string message, string exception)
        {
           await _loggerRepository.Log(message, exception);
           await _unitOfWork.CommitAsync();
        }
    }
}
