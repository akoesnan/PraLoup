using Facebook;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;

namespace PraLoup.FacebookObjects
{
    public class FacebookEventActions : IEventAction
    {
        private FacebookClient FbClient { get; set; }

        public FacebookEventActions(FacebookClient fbClient)
        {
            this.FbClient = fbClient;
        }

        public Event SendUpdate(Event ev)
        {
            return ev;
        }

        public Event SendReminder(Event ev)
        {
            return ev;
        }
    }
}
