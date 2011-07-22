using NHibernate.Criterion;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Query
{
    public class EventOrganizedByFriendQuery : IQuery<Event>
    {
        Account myAccount;

        public EventOrganizedByFriendQuery(Account myAccount)
        {
            this.myAccount = myAccount;
        }

        public QueryOver<Event> GetQuery()
        {
            var myFriends = QueryOver.Of<Connection>()
                .Where(c => c.MyId == myAccount.FacebookLogon.FacebookId)
                .Select(c => c.FriendId);

            var activityBelongToFriend = QueryOver.Of<Event>()
                .JoinQueryOver<Account>(a => a.Organizers)
                .WithSubquery.WhereProperty(c => c.FacebookLogon.FacebookId).In(myFriends);

            return activityBelongToFriend;
        }
    }
}
