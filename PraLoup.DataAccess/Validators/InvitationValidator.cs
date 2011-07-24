﻿using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Validation;

namespace PraLoup.DataAccess.Validators
{
    public class InvitationValidator : IValidator<Invitation>
    {
        public bool IsValid(Invitation entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(Invitation entity)
        {
            if (entity == null)
            {
                yield return "Invitation should not be null";
            }

            if (entity.Activity == null)
                yield return "Activity should not be null or empty";

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