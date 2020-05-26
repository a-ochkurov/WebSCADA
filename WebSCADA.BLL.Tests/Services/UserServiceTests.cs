using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using Moq;
using NUnit.Framework;
using WebSCADA.BLL.Services;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;
using WebSCADA.Domain.Models;
using WebSCADA.Web.Profiles;

namespace WebSCADA.BLL.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IRepository<User>> mockUserRepository;
        private Mock<IRepository<Role>> mockRoleRepository;
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
        public void Add_AddNewUser()
        {
            // Arrange
            mockUserRepository = new Mock<IRepository<User>>();
            mockRoleRepository = new Mock<IRepository<Role>>();
            var userService = new UserService(mockUserRepository.Object, mockRoleRepository.Object, mapper);
            var user = new UserDomain()
            {
                Login = "Login",
                Name = "Name",
                Password = "Pass",
                Role = "Admin"
            };

            // Act
            userService.Add(user);

            // Assert
            mockUserRepository.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void Get_GetUserByLoginAndPassword()
        {
            // Arrange
            mockUserRepository = new Mock<IRepository<User>>();
            mockRoleRepository = new Mock<IRepository<Role>>();
            var userService = new UserService(mockUserRepository.Object, mockRoleRepository.Object, mapper);
            mockUserRepository.Setup(x => x.Get(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User>());

            // Act
            userService.Get("Login", "Pass");

            // Assert
            mockUserRepository.Verify(x => x.Get(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }
    }
}
