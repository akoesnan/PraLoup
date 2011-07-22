using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Query;
using PraLoup.DataAccess.Services;

namespace PraLoup.BusinessLogic
{
    public static class EventExtensions
    {
        public static bool IsOrganizedByFriend(this Event e, Account account, IDataService ds)
        {
            var s = new EventOrganizedByFriendQuery(account);
            var q = s.GetQuery().Clone()
                .JoinQueryOver<Account>(c => c.Organizers)
                .Where(a => a == account);
            return ds.Event.ExecuteQuery(q).RowCount() > 0;
        }

        public static bool IsOrganizedByFriendOfFriend(this Event e, Account account, IDataService ds)
        {
            var s = new EventOrganizedByFriendOfFriendQuery(account);
            var q = s.GetQuery().Clone()
                    .JoinQueryOver<Account>(c => c.Organizers)
                    .Where(a => a == account);
            return ds.Event.ExecuteQuery(q).RowCount() > 0;
        }
    }
}
