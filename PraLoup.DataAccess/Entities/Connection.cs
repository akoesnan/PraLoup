using System;

namespace PraLoup.DataAccess.Entities
{
    public class Connection
    {
        public Connection() { }

        public Connection(long myId, long friendId)
        {
            this.MyId = myId;
            this.FriendId = friendId;
        }

        public virtual Guid Id { get; protected set; }

        public virtual long MyId { get; set; }

        public virtual long FriendId { get; set; }

        public override bool Equals(object obj)
        {
            var c = obj as Connection;
            if (c != null)
            {
                return c.Id == this.Id
                    && c.MyId == this.MyId
                    && c.FriendId == this.FriendId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
