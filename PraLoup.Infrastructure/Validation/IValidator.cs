using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.Infrastructure.Validation
{
    public interface IValidator<T> where T : class
    {
        bool IsValid(T entity);
        IEnumerable<string> BrokenRules(T entity);
    }
}
