using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Validation;


namespace PraLoup.DataAccess.Validators
{
    public class UserGroupValidator : IValidator<UserGroup>
    {
        public bool IsValid(UserGroup entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(UserGroup entity)
        {
            List<string> errors = new List<string>();
            if (String.IsNullOrEmpty(entity.Name ))
            {
                yield return "name cannot be null or empty";
            }
            if (entity.Business == null)
            {
                yield return "business cannot be null";
            }
            yield break;
        }
    }
}
