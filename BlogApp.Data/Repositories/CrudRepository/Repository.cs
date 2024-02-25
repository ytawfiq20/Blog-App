
using BlogApp.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlogApp.Data.Repositories.BlogRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> entities;
        public Repository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
            entities = _dbContext.Set<T>();
        }
        public T Add(T item)
        {
            entities.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public T Delete(Guid id)
        {
            T item = GetById(id);
            entities.Remove(item);
            _dbContext.SaveChanges();
            return item;
        }

        public T Edit(T item)
        {
            // entities.Update(item);
            var result = entities.Attach(item);
            result.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return item;
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetById(Guid id)
        {
            return entities.Find(id);
        }
    }
}
