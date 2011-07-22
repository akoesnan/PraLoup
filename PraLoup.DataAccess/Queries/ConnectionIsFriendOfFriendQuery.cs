using NHibernate.Criterion;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Query
{
    /// <summary>
    /// Decide if there is friend of friend
    /// </summary>
    public class ConnectionIsFriendOfFriendQuery : IQuery<Connection>
    {
        Account myAccount, friendAccount;

        public ConnectionIsFriendOfFriendQuery(Account myAccount, Account friendAccount)
        {
            this.myAccount = myAccount;
            this.friendAccount = friendAccount;
        }

        public QueryOver<Connection> GetQuery()
        {
            var myFriends = QueryOver.Of<Connection>()
                .Where(c => c.MyId == myAccount.FacebookLogon.FacebookId)
                .Select(c => c.FriendId);

            var isFriendsOfFriends = QueryOver.Of<Connection>()
                .Where(c => c.MyId == friendAccount.FacebookLogon.FacebookId)
                .WithSubquery.WhereProperty(c => c.FriendId).In(myFriends);

            return isFriendsOfFriends;
        }
    }
}
