using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Query;
using PraLoup.DataAccess.Services;

namespace PraLoup.BusinessLogic
{
    public static class AccountExtensions
    {
        public static bool IsFriend(this Account account, Account ab, IDataService ds)
        {
            var s = new ConnectionIsFriendQuery(account, ab);
            return ds.Connection.ExecuteQuery(s).RowCount() > 0;
        }

        public static bool IsFriendOfFriend(this Account account, Account ab, IDataService ds)
        {
            var q = new ConnectionIsFriendOfFriendQuery(account, ab);
            return ds.Connection.ExecuteQuery(q).RowCount() > 0;
        }

        public static ConnectionType GetConnection(this Account a, Event evt, IDataService ds)
        {
            ConnectionType ct = ConnectionType.NoConnection;

            if (evt.Organizers.Contains(a))
                ct |= ConnectionType.Owner;
            else if (evt.IsOrganizedByFriend(a, ds))
                ct |= ConnectionType.Friend;
            else if (evt.IsOrganizedByFriendOfFriend(a, ds))
                ct |= ConnectionType.FriendOfFriend;

            // TODO: is invited

            return ct;
        }        
    }
}
