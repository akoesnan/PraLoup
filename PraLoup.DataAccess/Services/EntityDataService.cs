using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using PraLoup.Infrastructure.Data;
using PraLoup.Infrastructure.Validation;

namespace PraLoup.DataAccess.Services
{
    public class EntityDataService<TEntity, TValidator>
        where TEntity : class
        where TValidator : IValidator<TEntity>
    {
        private readonly IRepository repository;
        private readonly TValidator validator;

        public EntityDataService(IRepository r, TValidator v)
        {
            this.repository = r;
            this.validator = v;
        }

        public IQueryOver<TEntity> ExecuteQuery(IQuery<TEntity> t)
        {
            return repository.ExecuteQuery<TEntity>(t);
        }

        public IQueryOver<TEntity> ExecuteQuery(QueryOver<TEntity> t)
        {
            return repository.ExecuteQuery<TEntity>(t);
        }

        public IQueryable<TEntity> GetQuery()
        {
            return repository.GetQuery<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return repository.GetAll<TEntity>();
        }

        public TEntity Find(object key)
        {
            return repository.Find<TEntity>(key);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.Where(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.FirstOrDefault<TEntity>(predicate);
        }

        public bool SaveOrUpdateAll(IEnumerable<TEntity> entities, out IEnumerable<string> brokenRules)
        {
            foreach (var entity in entities)
            {
                if (!validator.IsValid(entity))
                {
                    brokenRules = validator.BrokenRules(entity);
                    return false;
                }
                brokenRules = null;
                repository.SaveOrUpdate(entity);
            }
            brokenRules = null;
            return true;
        }

        public bool SaveOrUpdate(TEntity entity)
        {
            if (!validator.IsValid(entity))
            {
                return false;
            }
            repository.SaveOrUpdate(entity);
            return true;
        }

        public bool SaveOrUpdate(TEntity entity, out IEnumerable<string> brokenRules)
        {
            if (!validator.IsValid(entity))
            {
                brokenRules = validator.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            repository.SaveOrUpdate(entity);
            return true;
        }

        public void Delete(TEntity entity)
        {
            repository.Delete(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var e in entities)
            {
                repository.Delete(e);
            }
        }
    }
}
