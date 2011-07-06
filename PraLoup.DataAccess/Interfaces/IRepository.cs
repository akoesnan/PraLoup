using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Data.Entity;

namespace PraLoup.DataAccess.Interfaces
{
    public interface IRepository : IDisposable
    {

        DbContext Context { get; } 
        IQueryable<T> GetQuery <T>() where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        T Find<T>(params object[] keys) where T : class;
        IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
        T FirstOrDefault <T>(Expression<Func<T, bool>> preicate) where T : class;
        void Add<T>(T entity) where T:class;
        void AddAll<T>(IEnumerable<T> entities) where T : class;
        void Delete<T>(T entity) where T:class;
        void SaveChanges();
    }
}
