using NHibernate.Criterion;

namespace PraLoup.Infrastructure.Data
{
    public interface IQuery<T>
    {
        QueryOver<T> GetQuery();
    }

    public interface IQuery<T1, T2>
    {
        QueryOver<T1, T2> GetQuery();
    }
}
