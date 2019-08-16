using FriendBookApp.API.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FriendBookApp.API.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public Repository(DataContext dataContext)
        {
            DataContext = dataContext;
        }
        public DataContext DataContext { get; }

        public void Add(TEntity entity)
        {
            DataContext.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DataContext.AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DataContext.Set<TEntity>().Where(predicate);
        }

        public TEntity Get(int id)
        {
            return DataContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DataContext.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            DataContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DataContext.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            DataContext.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DataContext.Set<TEntity>().UpdateRange(entities);
        }
    }
}
