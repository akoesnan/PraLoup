using System.Collections.Generic;
using Facebook;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;

namespace PraLoup.FacebookObjects
{
    public class FacebookEventActions : IActivityAction
    {
        private FacebookClient FbClient { get; set; }

        public FacebookEventActions(FacebookClient fbClient)
        {
            this.FbClient = fbClient;
        }

        public Activity CreateActivityFromExistingEvent(Activity activity)
        {
            return activity;
        }

        public Activity CreateActivityFromNotExistingEvent(Activity activity)
        {
            return activity;
        }

        public IEnumerable<Invitation> Invite(Activity actv, IEnumerable<Invitation> invitations)
        {
            return invitations;
        }
    }
}
