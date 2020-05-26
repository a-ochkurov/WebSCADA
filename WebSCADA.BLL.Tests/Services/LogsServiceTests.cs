using AutoMapper;
using Moq;
using NUnit.Framework;
using WebSCADA.BLL.Services;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;
using WebSCADA.Web.Profiles;

namespace WebSCADA.BLL.Tests.Services
{
    [TestFixture]
    public class LogsServiceTests
    {
        private Mock<IRepository<Log>> mockLogsRepository;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntityDomainProfile>();
            });
            mapper = new Mapper(mapperConfiguration);
        }

        [Test]
        public void Get_GetLogData()
        {
            // Arrange
            mockLogsRepository = new Mock<IRepository<Log>>();
            var logsService = new LogsService(mockLogsRepository.Object, mapper);

            // Act
            var data = logsService.Get();

            // Assert
            mockLogsRepository.Verify(x => x.Get(), Times.Once);
        }
    }
}
