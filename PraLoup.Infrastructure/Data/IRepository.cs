using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;

namespace PraLoup.Infrastructure.Data
{
    public interface IRepository : IDisposable
    {

        IQueryable<T> GetQuery<T>() where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        T Find<T>(object keys) where T : class;
        IQueryOver<T> GetQueryOver<T>() where T : class;
        IQueryOver<T> ExecuteQuery<T>(QueryOver<T> queryOver) where T : class;
        IQueryOver<T> ExecuteQuery<T>(IQuery<T> spec) where T : class;
        IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> preicate) where T : class;
        T SaveOrUpdate<T>(T entity) where T : class;
        IEnumerable<T> SaveOrUpdateAll<T>(IEnumerable<T> entities) where T : class;
        void Delete<T>(T entity) where T : class;
        void SaveChanges();
    }
}
