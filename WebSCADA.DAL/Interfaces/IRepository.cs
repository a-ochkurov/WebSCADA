using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebSCADA.DAL.Interfaces
{
    public interface IRepository<TEntity>
       where TEntity : class
    {
        TEntity Get(string id);

        IList<TEntity> Get();

        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        void Create(TEntity item);

        void Update(string id, TEntity item);

        void Delete(string id);
    }
}
