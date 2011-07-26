using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Validation;

namespace PraLoup.DataAccess.Validators
{
    public class DealValidator : IValidator<Deal>
    {
        public bool IsValid(Deal entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(Deal entity)
        {
            if (entity.OriginalValue <= 0)
            {
                yield return "deal original value should be greater than 0";
            }
            if (entity.DealValue <= 0)
            {
                yield return "deal value should be greater than 0";
            }
            if (entity.DealValue >= entity.OriginalValue)
            {
                yield return "deal value should be less than original value";
            }
            if (entity.StartDateTime >= entity.EndDateTime)
            {
                yield return "deal start time should be before end date";
            }
            if (entity.StartDateTime.Date >= DateTime.UtcNow.Date)
            {
                yield return "start time cannot be in the past";
            }
            yield break;
        }
    }
}
