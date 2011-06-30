using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using PraLoup.DataAccess.Interfaces;

namespace PraLoup.DataAccess
{

    /// <summary>
    /// This Generic repository pattern is modified from:
    /// http://huyrua.wordpress.com/2010/07/13/entity-framework-4-poco-repository-and-specification-pattern/
    /// </summary>
    public class GenericRepository : IRepository
    {
        private DbContext DbContext;

        public GenericRepository(DbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public DbContext Context { get { return DbContext; } }

        public IQueryable<T> GetQuery<T>() where T : class
        {
            return this.DbContext.Set<T>();
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            var entities = this.DbContext.Set<T>().Select(o => o);
            return entities;
        }

        public T Find<T>(params object[] keys) where T : class
        {            
            return this.DbContext.Set<T>().Find(keys);
        }

        public IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {            
            return this.DbContext.Set<T>().Where(predicate);
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.DbContext.Set<T>().FirstOrDefault(predicate);
        }

        public void Add<T>(T entity) where T : class
        {
            this.DbContext.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this.DbContext.Set<T>().Remove(entity);
        }
      
        public void SaveChanges()
        {
            this.DbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.DbContext.Dispose();
        }
    }
}
