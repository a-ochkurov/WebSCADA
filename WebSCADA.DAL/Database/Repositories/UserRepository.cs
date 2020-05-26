using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;

namespace WebSCADA.DAL.Database.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IMongoCollection<User> users;


        public UserRepository(ScadaMongoDbContext context)
        {
            users = context.User;
        }

        public void Create(User item)
        {
            users.InsertOne(item);
        }

        public void Delete(string id)
        {
            users.DeleteOne(sch => sch.Id == id);
        }

        public User Get(string id)
        {
            return users.Find(sch => sch.Id == id).FirstOrDefault();
        }

        public IList<User> Get()
        {
            return users.Find(sch => true).ToList();
        }

        public IList<User> Get(Expression<Func<User, bool>> filter)
        {
            return users.AsQueryable().Where(filter.Compile()).ToList();
        }

        public void Update(string id, User item)
        {
            users.ReplaceOne(sch => sch.Id == id, item);
        }
    }
}
