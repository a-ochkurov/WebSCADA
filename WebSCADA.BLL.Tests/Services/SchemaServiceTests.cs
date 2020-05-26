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
    public class SchemaServiceTests
    {
        private Mock<IRepository<Schema>> mockSchemaRepository;
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
        public void AddNewSchema_CreateNewSchema()
        {
            // Arrange
            mockSchemaRepository = new Mock<IRepository<Schema>>();
            var schemaService = new SchemaService(mockSchemaRepository.Object, mapper);
            var schemaDomain = new SchemaDomain();
            // Act
            schemaService.AddNewSchema(schemaDomain);

            // Assert
            mockSchemaRepository.Verify(x => x.Create(It.IsAny<Schema>()), Times.Once);
        }

        [Test]
        public void DeleteSchema_DeleteSchemaByName()
        {
            // Arrange
            mockSchemaRepository = new Mock<IRepository<Schema>>();
            var list = new List<Schema>()
            {
                new Schema()
                {
                    Id = "1",
                    Data = "Data",
                    Name = "Name"
                },
            };
            mockSchemaRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Schema, bool>>>())).Returns(list);
            var schemaService = new SchemaService(mockSchemaRepository.Object, mapper);
            var schemaName = "Name";

            // Act
            schemaService.DeleteSchema(schemaName);

            // Assert
            mockSchemaRepository.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetSchema_GetSchemaByName()
        {
            // Arrange
            mockSchemaRepository = new Mock<IRepository<Schema>>();
            var list = new List<Schema>()
            {
                new Schema()
                {
                    Id = "1",
                    Data = "Data",
                    Name = "Name"
                },
            };
            mockSchemaRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Schema, bool>>>())).Returns(list);
            var schemaService = new SchemaService(mockSchemaRepository.Object, mapper);
            var schemaDomain = new SchemaDomain();

            // Act
            var res = schemaService.GetSchema("Name");

            // Assert
            mockSchemaRepository.Verify(x => x.Get(It.IsAny<Expression<Func<Schema, bool>>>()), Times.Once);
        }

        [Test]
        public void SaveSchema_ShouldCreateNewSchema()
        {
            // Arrange
            mockSchemaRepository = new Mock<IRepository<Schema>>();
            var schemaService = new SchemaService(mockSchemaRepository.Object, mapper);
            var schemaDomain = new SchemaDomain()
            {
                Data = "Data",
                Name = "Name"
            };

            // Act
            schemaService.SaveSchema("Name", schemaDomain);

            // Assert
            mockSchemaRepository.Verify(x => x.Create(It.IsAny<Schema>()), Times.Once);
        }

        [Test]
        public void SaveSchema_ShouldUpdateSchema()
        {
            // Arrange
            mockSchemaRepository = new Mock<IRepository<Schema>>();
            var schemaService = new SchemaService(mockSchemaRepository.Object, mapper);
            var schemaDomain = new SchemaDomain()
            {
                Id = "ID",
                Data = "Data",
                Name = "Name"
            };

            // Act
            schemaService.SaveSchema("Name", schemaDomain);

            // Assert
            mockSchemaRepository.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Schema>()), Times.Once);
        }
    }
}
