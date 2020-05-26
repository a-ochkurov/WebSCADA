using System.Collections.Generic;
using AutoMapper;
using WebSCADA.BLL.Interfaces;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;
using WebSCADA.Domain.Models;

namespace WebSCADA.BLL.Services
{
    public class LogsService : ILogsService
    {
        private readonly IRepository<Log> logsRepository;
        private readonly IMapper mapper;

        public LogsService(IRepository<Log> logsRepository, IMapper mapper)
        {
            this.logsRepository = logsRepository;
            this.mapper = mapper;
        }

        public List<LogDomain> Get()
        {
            return mapper.Map<List<LogDomain>>(logsRepository.Get());
        }
    }
}
