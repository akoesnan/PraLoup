using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Validation;

namespace PraLoup.DataAccess.Validators
{
    public class BusinessValidator : IValidator<Business>
    {
        public bool IsValid(Business entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(Business entity)
        {
            if (String.IsNullOrEmpty(entity.Name))
            {
                yield return "business name should not be emtpy";
            }
            if (entity.Address == null)
            {
                yield return "business address should not be null";
            }
            //if (entity.FacebookLogon == null)
            //{
            //    yield return "Facebook Logon should not be null";
            //}
            //else
            //{
            //    if (entity.FacebookLogon.FacebookId == default(long))
            //    {
            //        yield return "Facebook Logon should not be null and the facebook id should be valid ";
            //    }
            //    if (String.IsNullOrEmpty(entity.FacebookLogon.AccessToken))
            //    {
            //        yield return "Facebook access tokens should be populated";
            //    }
            //}
            yield break;
        }
    }
}
