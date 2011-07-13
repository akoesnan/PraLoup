using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Validation;

namespace PraLoup.DataAccess.Validators
{
    public class AccountValidator : IValidator<Account>
    {
        public bool IsValid(Account entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(Account entity)
        {
            if (entity.FacebookLogon != null && entity.FacebookLogon.FacebookId != default(long))
            {
                yield return "Facebook Logon should not be null and the facebook id should be valid ";
            }

            yield break;
        }
    }
}
