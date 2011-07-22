using NHibernate.Criterion;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Query
{
    public class ActivityOrganizedByFriendQuery : IQuery<Activity>
    {
        Account myAccount;

        public ActivityOrganizedByFriendQuery(Account myAccount)
        {
            this.myAccount = myAccount;
        }

        public QueryOver<Activity> GetQuery()
        {
            var myFriends = QueryOver.Of<Connection>()
                .Where(c => c.MyId == myAccount.FacebookLogon.FacebookId)
                .Select(c => c.FriendId);

            var activityBelongToFriend = QueryOver.Of<Activity>()
                .JoinQueryOver<Account>(a => a.Organizer)
                .WithSubquery.WhereProperty(c => c.FacebookLogon.FacebookId).In(myFriends);

            return activityBelongToFriend;
        }
    }
}
