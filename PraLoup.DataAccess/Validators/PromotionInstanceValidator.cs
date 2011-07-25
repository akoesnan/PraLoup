using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Validation;
using System;

namespace PraLoup.DataAccess.Validators
{
    public class PromotionInstanceValidator : IValidator<PromotionInstance>
    {
        public bool IsValid(PromotionInstance entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(PromotionInstance entity)
        {
            if (entity.Promotion == null)
            {
                yield return "Event promotion not be null";
            }
            else
            {
                if (entity.Promotion.Id == null)
                    yield return "promotion should be an exsiting promotion";
            }
            if (entity.Sender == null)
                yield return "Sender should not be null or empty";

            if (entity.Recipient == null)
                yield return "Recipient should not be null or empty";

            if (String.IsNullOrEmpty(entity.Message))
                yield return "Message should not be null or empty";

            if (entity.CreateDateTime == default(DateTime))
                yield return "CreatedDateTime should be setup";
            yield break;
        }
    }
}
