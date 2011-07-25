using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Validation;

namespace PraLoup.DataAccess.Validators
{
    public class PromotionValidator : IValidator<Promotion>
    {
        public bool IsValid(Promotion entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(Promotion entity)
        {
            if (entity.Event == null)
            {
                yield return "Event should not be null";
            }
            else if(entity.Taken < 0)
            {
                yield return "taken count cannot not be negative";
            }
            if (entity.Available < 0)
            {
                yield return "Available count cannot be negative";
            }
            
            yield break;
        }
    }
}
