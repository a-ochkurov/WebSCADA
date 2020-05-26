using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;

namespace WebSCADA.DAL.Database.Repositories
{
    public class SchemaRepository : IRepository<Schema>
    {
        private readonly IMongoCollection<Schema> Schema;

        public SchemaRepository(ScadaMongoDbContext context)
        {
            Schema = context.Schema;
        }

        public void Create(Schema item)
        {
            Schema.InsertOne(item);
        }

        public void Delete(string id)
        {
            Schema.DeleteOne(sch => sch.Id == id);
        }

        public Schema Get(string id)
        {
            return Schema.Find(sch => sch.Id == id).FirstOrDefault();
        }

        public IList<Schema> Get()
        {
            return Schema.Find(sch => true).ToList();
        }

        public IList<Schema> Get(Expression<Func<Schema, bool>> filter)
        {
            return Schema.AsQueryable().Where(filter.Compile()).ToList();
        }

        public void Update(string id, Schema item)
        {
            Schema.ReplaceOne(sch => sch.Id == id, item);
        }
    }
}
