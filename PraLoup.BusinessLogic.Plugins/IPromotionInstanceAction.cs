using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic.Plugins
{
    public interface IPromotionInstanceAction
    {
        IEnumerable<PromotionInstance> CreatePromoInstance(Promotion promo, IEnumerable<Account> invites, string message);

        IEnumerable<PromotionInstance> Forward(PromotionInstance promotionInstance, IEnumerable<Account> invites, string message);

        PromotionInstance Accept(PromotionInstance pi, string message);
        PromotionInstance Decline(PromotionInstance pi, string message);
        PromotionInstance Maybe(PromotionInstance pi, string message);

        PromotionInstance Response(PromotionInstance promotionInstance);

        
        IList<PromotionInstance> GetAvailableInvitationsForUser();
        IList<PromotionInstance> GetAvailableInvitationsForUser(Guid eventId);
        IList<PromotionInstance> GetUserInvitesForPromotion(Promotion promo);
      
    }
}
