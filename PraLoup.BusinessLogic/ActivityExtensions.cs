using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Query;
using PraLoup.DataAccess.Services;

namespace PraLoup.BusinessLogic
{
    public static class ActivityExtensions
    {
        public static bool IsOrganizedByFriend(this Activity e, Account account, IDataService ds)
        {
            var s = new ActivityOrganizedByFriendQuery(account);
            var q = s.GetQuery().Clone()
                .JoinQueryOver<Account>(c => c.Organizer)
                .Where(a => a == account);
            return ds.Activity.ExecuteQuery(q).RowCount() > 0;
        }

        public static bool IsOrganizedByFriendOfFriend(this Activity e, Account account, IDataService ds)
        {
            var s = new ActivityOrganizedByFriendOfFriendQuery(account);
            var q = s.GetQuery().Clone()
                    .JoinQueryOver<Account>(c => c.Organizer)
                    .Where(a => a == account);
            return ds.Activity.ExecuteQuery(q).RowCount() > 0;
        }
    }
}
