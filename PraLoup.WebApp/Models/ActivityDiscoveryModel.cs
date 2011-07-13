using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;
using PraLoup.BusinessLogic;

namespace PraLoup.WebApp.Models
{
    public class ActivityDiscoveryModel
    {
        public IEnumerable<Activity> MyActivity { get; private set; }
        public IEnumerable<ActivityViewModel> MyOtherActivity { get; private set; }
        public AccountBase Account { get; private set; }

        public ActivityDiscoveryModel(AccountBase account)
        {
            this.Account = account;
        }

        public void Setup()
        {
            this.MyActivity = Account.ActivityActions.GetMyActivities(0, 10).ToList();

            // TODO: edit this linq so that we do the calculation in the db layer
            this.MyOtherActivity = this.Account.ActivityActions.GetAllActivies()
                .Where(a => this.Account.GetPermissions(a).HasFlag(Permissions.View))
                .Select(a => new ActivityViewModel(a, this.Account.GetPermissions(a))).ToList();
        }
    }

    public class ActivityViewModel : BaseModel
    {
        public Activity Activity { get; private set; }

        public ActivityViewModel(Activity a, Permissions p)
            : base(p)
        {
            this.Activity = a;
        }
    }
}
