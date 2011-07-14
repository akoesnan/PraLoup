using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.Infrastructure.Data
{
    public interface ISpecification<T>
    {
        T First (IQueryable<T> query);
        
        T FirstOrDefault(IQueryable<T> query);

        bool Any(IQueryable<T> query);

        IQueryable<T> Where (IQueryable<T> query);
    }
}
