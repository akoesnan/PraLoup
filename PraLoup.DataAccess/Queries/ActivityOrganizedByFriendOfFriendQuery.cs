using NHibernate.Criterion;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Query
{
    public class ActivityOrganizedByFriendOfFriendQuery : IQuery<Activity>
    {
        Account myAccount, friendAccount;

        public ActivityOrganizedByFriendOfFriendQuery(Account myAccount)
        {
            this.myAccount = myAccount;
        }

        public QueryOver<Activity> GetQuery()
        {
            var myFriends = QueryOver.Of<Connection>()
                .Where(c => c.MyId == myAccount.FacebookLogon.FacebookId)
                .Select(c => c.FriendId);

            var isFriendsOfFriends = QueryOver.Of<Connection>()
                .WithSubquery.WhereProperty(c => c.MyId).In(myFriends)
                .Select(Projections.Distinct(
                    Projections.ProjectionList().Add(
                        Projections.Property<Connection>(c => c.FriendId))));

            var activityBelongToFriendOfFriend = QueryOver.Of<Activity>()
              .JoinQueryOver<Account>(a => a.Organizer)
              .WithSubquery.WhereProperty(c => c.FacebookLogon.FacebookId).In(isFriendsOfFriends);

            return activityBelongToFriendOfFriend;
        }
    }
}
