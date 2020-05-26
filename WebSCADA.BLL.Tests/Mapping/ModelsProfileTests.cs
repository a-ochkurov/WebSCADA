using AutoMapper;
using NUnit.Framework;
using WebSCADA.DAL.Entities;
using WebSCADA.Domain.Models;
using WebSCADA.Web.Profiles;

namespace WebSCADA.BLL.Tests.Mapping
{
    [TestFixture]
    public class ModelSProfileTests
    {
        private MapperConfiguration mapperConfiguration;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntityDomainProfile>();
            });
            mapper = new Mapper(mapperConfiguration);
        }

        [Test]
        public void AutoMapperConfigurationIsValid()
        {
            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Test]
        public void SchemaDmainToSchema_MappingIsValid()
        {
            // Arrange
            var schemaDmain = new SchemaDomain()
            {
                Id = "Id",
                Name = "Test Name",
                Data = "Test Data"
            };

            // Act
            var schema = mapper.Map<Schema>(schemaDmain);

            // Assert
            Assert.AreEqual(schemaDmain.Id, schema.Id);
            Assert.AreEqual(schemaDmain.Name, schema.Name);
            Assert.AreEqual(schemaDmain.Data, schema.Data);
        }

        [Test]
        public void LogDmainToLog_MappingIsValid()
        {
            // Arrange
            var logDmain = new LogDomain()
            {
                Id = "Tets Id",
                Date = "20.08.2020",
                Message = "Test Message"
            };

            // Act
            var log = mapper.Map<Log>(logDmain);

            // Assert
            Assert.AreEqual(logDmain.Id, log.Id);
            Assert.AreEqual(logDmain.Date, log.Date);
            Assert.AreEqual(logDmain.Message, log.Message);
        }

        [Test]
        public void UserDmainToUser_MappingIsValid()
        {
            // Arrange
            var userDmain = new UserDomain()
            {
                Id = "Tets Id",
                Name = "Andrey",
                Login = "Andrey123",
                Password = "HashPassword"
            };

            // Act
            var user = mapper.Map<User>(userDmain);

            // Assert
            Assert.AreEqual(userDmain.Id, user.Id);
            Assert.AreEqual(userDmain.Name, user.Name);
            Assert.AreEqual(userDmain.Login, user.Login);
            Assert.AreEqual(userDmain.Password, user.Password);
        }
    }
}
