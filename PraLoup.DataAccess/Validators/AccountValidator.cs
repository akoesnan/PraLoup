using System;
using System.Collections.Generic;
using System.Linq;
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
            if (entity.FacebookLogon == null)
            {
                yield return "Facebook Logon should not be null";
            }
            else
            {
                if (entity.FacebookLogon.FacebookId == default(long))
                {
                    yield return "Facebook Logon should not be null and the facebook id should be valid ";
                }
                if (String.IsNullOrEmpty(entity.FacebookLogon.AccessToken))
                {
                    yield return "Facebook access tokens should be populated";
                }
            }
            yield break;
        }
    }
}
