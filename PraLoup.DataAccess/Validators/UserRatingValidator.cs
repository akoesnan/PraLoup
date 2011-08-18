
using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Validation;

namespace PraLoup.DataAccess.Validators
{
    public class UserRatingValidator : IValidator<UserRating>
    {
        public bool IsValid(UserRating entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(UserRating entity)
        {
            if (entity.RatedBy == null)
            {
                yield return "Rated by cannot be null";
            }
            else if (entity.User == null)
            {
                yield return "User by cannot be null";
            }

            yield break;
        }
    }
}
