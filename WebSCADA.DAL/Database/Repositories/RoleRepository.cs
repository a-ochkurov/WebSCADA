using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;

namespace WebSCADA.DAL.Database.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly IMongoCollection<Role> roles;

        public RoleRepository(ScadaMongoDbContext context)
        {
            roles = context.Role;
        }

        public void Create(Role item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Role Get(string id)
        {
            throw new NotImplementedException();
        }

        public IList<Role> Get()
        {
            return roles.Find(sch => true).ToList();
        }

        public IList<Role> Get(Expression<Func<Role, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, Role item)
        {
            throw new NotImplementedException();
        }
    }
}
