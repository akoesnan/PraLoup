using System.Collections.Generic;
using System.Dynamic;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.Utilities;
using PraLoup.Plugins;
using Facebook;

namespace PraLoup.FacebookObjects
{
    public class FacebookEventActions : IEventAction
    {
        private FacebookClient FbClient { get; set; }

        public FacebookEventActions(FacebookClient fbClient) {
            this.FbClient = fbClient;
        }

        public Activity CreateActivityFromExistingEvent(Activity activity)
        {
            throw new System.NotImplementedException();
        }

        public Activity CreateActivityFromNotExistingEvent(Activity activity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Invitation> Invite(Activity actv, IEnumerable<Invitation> invitations)
        {
            throw new System.NotImplementedException();
        }
    }
}
