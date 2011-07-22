using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.Infrastructure.Validation;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataAccess.Validators
{
    public class MetroAreaValidator : IValidator<MetroArea>
    {
        public bool IsValid(MetroArea entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(MetroArea entity)
        {
            if (entity != null)
                yield return "MetroArea should not be null";
            if (!String.IsNullOrEmpty(entity.City))
                yield return "City should not be null or empty";
            if (!String.IsNullOrEmpty(entity.State))
                yield return "State should not be null or empty";
            if (!String.IsNullOrEmpty(entity.Country))
                yield return "Country should not be null or empty";
            yield break;
        }
    }
}
