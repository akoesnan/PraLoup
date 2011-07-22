using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Validation;

namespace PraLoup.DataAccess.Validators
{
    public class ConnectionValidator : IValidator<Connection>
    {
        public bool IsValid(Connection entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(Connection entity)
        {
            yield break;
        }
    }
}
