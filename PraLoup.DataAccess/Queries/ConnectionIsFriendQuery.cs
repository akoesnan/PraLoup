using NHibernate.Criterion;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Query
{
    public class ConnectionIsFriendQuery : IQuery<Connection>
    {
        Account myAccount, friendAccount;

        public ConnectionIsFriendQuery(Account myAccount, Account friendAccount)
        {
            this.myAccount = myAccount;
            this.friendAccount = friendAccount;
        }

        public QueryOver<Connection> GetQuery()
        {
            return QueryOver.Of<Connection>()
                .Where(c => c.MyId == myAccount.FacebookLogon.FacebookId)
                .And(c => c.FriendId == friendAccount.FacebookLogon.FacebookId);
        }
    }
}
