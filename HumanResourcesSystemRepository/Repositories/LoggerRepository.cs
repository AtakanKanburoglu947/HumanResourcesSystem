using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemRepository.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly AppDbContext _appDbContext;
        public LoggerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Log(string message, string exception)
        {
            DatabaseLog databaseLog = new DatabaseLog { Message = message, Exception = exception };
            await _appDbContext.DatabaseLogs.AddAsync(databaseLog);
        }
    }
}
