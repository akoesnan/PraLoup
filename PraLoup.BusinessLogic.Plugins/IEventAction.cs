using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic.Plugins
{
    public interface IEventAction
    {
        Event SendUpdate(Event ev);
        Event SendReminder(Event ev);
    }
}
