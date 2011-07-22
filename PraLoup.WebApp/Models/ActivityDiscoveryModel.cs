using System.Collections.Generic;
using System.Linq;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;

namespace PraLoup.WebApp.Models
{
    public class ActivityDiscoveryModel
    {
        public IEnumerable<Activity> MyActivity { get; private set; }
        public IEnumerable<Activity> MyFriendActivity { get; private set; }
        public IEnumerable<Activity> MyFriendOfFriendActivity { get; private set; }
        public AccountBase Account { get; private set; }

        public ActivityDiscoveryModel(AccountBase account)
        {
            this.Account = account;
        }

        public void Setup()
        {
            this.MyActivity = Account.ActivityActions.GetMyActivities(0, 10).ToList();
            this.MyFriendActivity = Account.ActivityActions.GetFriendsActivities(0, 10);
            this.MyFriendOfFriendActivity = Account.ActivityActions.GetFriendsOfFriendsActivities(0, 10);
        }
    }

    public class ActivityViewModel : BaseModel
    {
        public Activity Activity { get; private set; }

        public ActivityViewModel(Activity a, Permission p)
            : base(p)
        {
            this.Activity = a;
        }
    }
}
