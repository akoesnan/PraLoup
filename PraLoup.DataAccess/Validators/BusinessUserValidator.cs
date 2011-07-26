using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Validation;

namespace PraLoup.DataAccess.Validators
{
    public class BusinessUserValidator : IValidator<BusinessUser>
    {
        public bool IsValid(BusinessUser entity)
        {
            return BrokenRules(entity).Count() == 0;
        }
        
        public IEnumerable<string> BrokenRules(BusinessUser entity)
        {
            List<string> errors = new List<string>();
            if (entity.User == null)
            {
                yield return "User cannot be null";
            }
            yield break;
        }
    }
}
