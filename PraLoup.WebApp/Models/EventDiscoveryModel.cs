using System.Collections.Generic;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Models
{
    public class EventDiscoveryModel
    {

        public string Location { get; set; }
        public IEnumerable<Event> MyEvent { get; private set; }
        public IEnumerable<Event> MyFriendEvent { get; private set; }
        public IEnumerable<Event> MyFriendOfFriendEvent { get; private set; }
        public IEnumerable<Event> PublicEvent { get; private set; }

        private AccountBase account;

        public EventDiscoveryModel(AccountBase ab)
        {
            this.account = ab;
            Setup();
        }

        private void Setup()
        {
            Location = "Seattle";
            MyEvent = this.account.EventActions.GetMyEvents(0, 10);
            MyFriendEvent = this.account.EventActions.GetMyFriendsEvents(0, 10);
            MyFriendOfFriendEvent = this.account.EventActions.GetMyFriendsOfFriendsEvents(0, 10);
            PublicEvent = this.account.EventActions.GetPublicEvents(0, 10);
        }
    }
}