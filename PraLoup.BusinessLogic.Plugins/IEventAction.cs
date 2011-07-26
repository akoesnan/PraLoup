using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic.Plugins
{
    public interface IPromotionAction
    {
        Event SendUpdate(Promotion ev);
        Event SendReminder(Promotion ev);
    }
}
