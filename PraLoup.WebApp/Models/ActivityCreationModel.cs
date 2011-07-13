using PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Models
{
    public class ActivityCreationModel
    {
        public Activity Activity { get; private set; }
        public Event Event { get; private set; }

        public ActivityCreationModel(Activity activity, Event ev)
        {
            this.Activity = activity;
            this.Event = ev;
        }

        public void InvitePeople()
        {
        }
    }
}