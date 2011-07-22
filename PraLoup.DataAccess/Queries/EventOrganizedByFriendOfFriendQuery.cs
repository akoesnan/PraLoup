using NHibernate.Criterion;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Query
{
    public class EventOrganizedByFriendOfFriendQuery : IQuery<Event>
    {
        Account myAccount, friendAccount;

        public EventOrganizedByFriendOfFriendQuery(Account myAccount)
        {
            this.myAccount = myAccount;
        }

        public QueryOver<Event> GetQuery()
        {
            var myFriends = QueryOver.Of<Connection>()
                .Where(c => c.MyId == myAccount.FacebookLogon.FacebookId)
                .Select(c => c.FriendId);

            var isFriendsOfFriends = QueryOver.Of<Connection>()
                .WithSubquery.WhereProperty(c => c.MyId).In(myFriends)
                .Select(Projections.Distinct(
                    Projections.ProjectionList().Add(
                        Projections.Property<Connection>(c => c.FriendId))));

            var eventOrganizedByFriendOfFriend = QueryOver.Of<Event>()
              .JoinQueryOver<Account>(a => a.Organizers)
              .WithSubquery.WhereProperty(c => c.FacebookLogon.FacebookId).In(isFriendsOfFriends);

            return eventOrganizedByFriendOfFriend;
        }
    }
}
