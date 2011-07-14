using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Predicate { get; set; }

        public Specification()
        {
        }

        public And<T> And(Specification<T> specification)
        {
            return new And<T>(this, specification);
        }

        public Or<T> Or(Specification<T> specification)
        {
            return new Or<T>(this, specification);
        }

        public bool Any(IQueryable<T> entities)
        {
            return entities.Any(this.IsSatisfied());
        }

        public T First(IQueryable<T> entities)
        {
            return entities.First(this.IsSatisfied());
        }

        public T FirstOrDefault(IQueryable<T> entities)
        {
            return entities.FirstOrDefault(this.IsSatisfied());
        }

        public IQueryable<T> Where(IQueryable<T> entities)
        {
            return entities.Where(this.IsSatisfied());
        }

        public abstract Expression<Func<T, bool>> IsSatisfied();
    }

    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        protected readonly Specification<T> leftOperand;
        protected readonly Specification<T> rightOperand;

        public CompositeSpecification(Specification<T> left, Specification<T> right)
        {
            this.leftOperand = left;
            this.rightOperand = right;
        }

        public T First(IQueryable<T> query)
        {
            return this.Where(query).First();
        }

        public T FirstOrDefault(IQueryable<T> query)
        {
            return this.Where(query).FirstOrDefault();
        }

        public bool Any(IQueryable<T> query)
        {
            return this.Where(query).FirstOrDefault() != null;
        }

        public abstract IQueryable<T> Where(IQueryable<T> query);
    }

    public class And<T> : CompositeSpecification<T>
    {
        public And(Specification<T> left, Specification<T> right)
            : base(left, right)
        {
        }

        public override IQueryable<T> Where(IQueryable<T> entities)
        {
            return entities.Where(leftOperand.Predicate.And(rightOperand.Predicate));
        }
    }

    public class Or<T> : CompositeSpecification<T>
    {
        public Or(Specification<T> left, Specification<T> right)
            : base(left, right)
        {
        }

        public override IQueryable<T> Where(IQueryable<T> entities)
        {
            return entities.Where(leftOperand.Predicate.Or(rightOperand.Predicate));
        }
    }

    public static class ExpressionExtension
    {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }
    }

    public class ParameterRebinder : ExpressionVisitor
    {

        private readonly Dictionary<ParameterExpression, ParameterExpression> map;



        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {

            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();

        }



        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {

            return new ParameterRebinder(map).Visit(exp);

        }



        protected override Expression VisitParameter(ParameterExpression p)
        {

            ParameterExpression replacement;

            if (map.TryGetValue(p, out replacement))
            {

                p = replacement;

            }

            return base.VisitParameter(p);

        }

    }
}


