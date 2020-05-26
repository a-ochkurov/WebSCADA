using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;

namespace WebSCADA.DAL.Database.Repositories
{
    public class LogsRepository : IRepository<Log>
    {
        private readonly IMongoCollection<Log> logs;

        public LogsRepository(ScadaMongoDbContext context)
        {
            logs = context.Log;
        }

        public void Create(Log item)
        {
            logs.InsertOne(item);
        }

        public void Delete(string id)
        {
            logs.DeleteOne(sch => sch.Id == id);
        }

        public Log Get(string id)
        {
            return logs.Find(sch => sch.Id == id).FirstOrDefault();
        }

        public IList<Log> Get()
        {
            return logs.Find(sch => true).ToList();
        }

        public IList<Log> Get(Expression<Func<Log, bool>> filter)
        {
            return logs.AsQueryable().Where(filter.Compile()).ToList();
        }

        public void Update(string id, Log item)
        {
            logs.ReplaceOne(sch => sch.Id == id, item);
        }
    }
}
