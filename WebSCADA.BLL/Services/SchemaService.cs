using System.Linq;
using AutoMapper;
using WebSCADA.BLL.Interfaces;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;
using WebSCADA.Domain.Models;

namespace WebSCADA.BLL.Services
{
    public class SchemaService : ISchemaService
    {
        private readonly IRepository<Schema> schemaRepository;
        private readonly IMapper mapper;

        public SchemaService(IRepository<Schema> schemaRepository, IMapper mapper)
        {
            this.schemaRepository = schemaRepository;
            this.mapper = mapper;
        }

        public void AddNewSchema(SchemaDomain schemaRequest)
        {
            var shema = new Schema()
            {
                Data = schemaRequest.Data,
                Name = schemaRequest.Name,
            };

            schemaRepository.Create(shema);
        }

        public void DeleteSchema(string name)
        {
            var schema = schemaRepository.Get(s => s.Name == name).FirstOrDefault();

            schemaRepository.Delete(schema.Id);
        }

        public SchemaDomain GetSchema(string name)
        {
            var schema = schemaRepository.Get(s => s.Name == name).FirstOrDefault();
            var schemaDomain = mapper.Map<SchemaDomain>(schema);

            return schemaDomain;
        }

        public void SaveSchema(string name, SchemaDomain schemaRequest)
        {
            if (string.IsNullOrEmpty(schemaRequest.Id))
            {
                var shema = new Schema()
                {
                    Data = schemaRequest.Data,
                    Name = schemaRequest.Name,
                };

                schemaRepository.Create(shema);
            }
            else
            {
                var shema = new Schema()
                {
                    Id = schemaRequest.Id,
                    Data = schemaRequest.Data,
                    Name = schemaRequest.Name,
                };

                schemaRepository.Update(schemaRequest.Id, shema);
            }
        }
    }
}
