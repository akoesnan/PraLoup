using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess
{
    /// <summary>
    /// Generic Repository is an abstraction of database in our system.
    /// </summary>
    public class GenericRepository : IRepository
    {
        private ISession dbSession;

        public GenericRepository(ISession session)
        {
            dbSession = session;
        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (dbSession.Transaction == null)
            {
                using (var t = dbSession.BeginTransaction())
                {
                    var entities = dbSession.Query<T>().Where(predicate);
                    return entities;
                }
            }
            else
            {
                return dbSession.Query<T>().Where(predicate);
            }
        }

        public T First<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (dbSession.Transaction == null)
            {
                using (var t = dbSession.BeginTransaction())
                {
                    var entity = dbSession.Query<T>().First(predicate);
                    return entity;
                }
            }
            else
            {
                return dbSession.Query<T>().First(predicate);
            }
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (dbSession.Transaction == null)
            {
                using (var t = dbSession.BeginTransaction())
                {
                    var entity = dbSession.Query<T>().FirstOrDefault(predicate);
                    return entity;
                }
            }
            else
            {
                return dbSession.Query<T>().FirstOrDefault(predicate);
            }
        }

        public T SaveOrUpdate<T>(T entity) where T : class
        {
            if (dbSession.Transaction == null)
            {
                using (var t = dbSession.BeginTransaction())
                {
                    dbSession.SaveOrUpdate(entity);
                    t.Commit();
                    return entity;
                }
            }
            else
            {
                dbSession.SaveOrUpdate(entity);
                return entity;
            }
        }

        public IEnumerable<T> SaveOrUpdateAll<T>(IEnumerable<T> entities) where T : class
        {
            if (dbSession.Transaction == null)
            {
                using (var t = dbSession.BeginTransaction())
                {
                    foreach (var e in entities)
                    {
                        dbSession.SaveOrUpdate(e);
                    }
                    t.Commit();
                    return entities;
                }
            }
            else
            {
                foreach (var e in entities)
                {
                    dbSession.SaveOrUpdate(e);
                }
                return entities;
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            if (dbSession.Transaction == null)
            {
                using (var t = dbSession.BeginTransaction())
                {
                    dbSession.Delete(entity);
                    t.Commit();
                }
            }
            else
            {
                dbSession.Delete(entity);
            }
        }

        public void SaveChanges()
        {
            if (dbSession.Transaction != null && !dbSession.Transaction.WasCommitted)
            {
                dbSession.Transaction.Commit();
            }
        }

        public void Dispose()
        {
            dbSession.Dispose();
        }

        public IQueryable<T> GetQuery<T>() where T : class
        {
            return dbSession.Query<T>();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            if (dbSession.Transaction == null)
            {
                using (var t = dbSession.BeginTransaction())
                {
                    var entities = dbSession.Query<T>().Select(o => o);
                    t.Commit();
                    return entities;
                }
            }
            else
            {
                return dbSession.Query<T>().Select(o => o);
            }
        }

        public T Find<T>(object id) where T : class
        {
            return dbSession.Get<T>(id);
        }


        public IQueryOver<T> GetQueryOver<T>() where T : class
        {
            return dbSession.QueryOver<T>();
        }

        public IQueryOver<T> ExecuteQuery<T>(QueryOver<T> query) where T : class
        {
            return query.GetExecutableQueryOver(dbSession);
        }

        public IQueryOver<T1, T1> ExecuteQuery<T1, T2>(QueryOver<T1, T2> query)
            where T1 : class
            where T2 : class
        {
            return query.GetExecutableQueryOver(dbSession);
        }

        public IQueryOver<T> ExecuteQuery<T>(IQuery<T> spec) where T : class
        {
            return spec.GetQuery().GetExecutableQueryOver(dbSession);
        }
    }
}
