using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic.Plugins
{
    public interface IActivityAction
    {
        Activity CreateActivityFromExistingEvent(Activity activity);

        Activity CreateActivityFromNotExistingEvent(Activity activity);

        Activity SendUpdate(Activity activity);

        Activity SendReminder(Activity activity);
    }
}
