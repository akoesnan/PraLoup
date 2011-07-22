using NHibernate.Criterion;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Query
{
    public class AccountGetFriendQuery : IQuery<Account>
    {
        Account myAccount;

        public AccountGetFriendQuery(Account myAccount)
        {
            this.myAccount = myAccount;
        }

        public QueryOver<Account> GetQuery()
        {
            var friendIds = QueryOver.Of<Connection>()
               .Where(c => c.MyId == myAccount.FacebookLogon.FacebookId)
               .Select(c => c.FriendId);

            return QueryOver.Of<Account>()
                .WithSubquery.WhereProperty(c => c.FacebookLogon.FacebookId).In(friendIds);
        }
    }
}
