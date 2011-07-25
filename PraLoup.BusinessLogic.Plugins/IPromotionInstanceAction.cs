using System.Collections.Generic;
using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic.Plugins
{
    public interface IPromotionInstanceAction
    {
        IEnumerable<PromotionInstance> Forward(PromotionInstance promotionInstance, IEnumerable<Account> invites, string message);

        PromotionInstance Response(PromotionInstance promotionInstance);


    }
}
