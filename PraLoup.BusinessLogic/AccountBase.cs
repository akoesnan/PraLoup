using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using System.Web.Security;


namespace PraLoup.BusinessLogic
{
    public class AccountBase : MembershipUser
    {
        protected Account account = null;

        protected string[] _friends = null;

        protected static object mutex = new object();
    

        public string[] Friends
        {
            get
            {
                if (_friends == null)
                {
                    lock(mutex)
                    {
                        if (_friends == null)
                        {
                            if (String.IsNullOrEmpty(account.Friends))
                            {
                                _friends = new string[] { };
                            }
                            else
                            {
                                _friends = account.Friends.Split(new char[] { ';' });
                            }
                        }
                    }
                }
                return _friends;
            }
        }

        public bool IsCreated()
        {
            EntityRepository er = new EntityRepository();

            var result = from a in er.Accounts
                         where
                             a.UserId == account.UserId
                         select a;

            if (result.Count<Account>() != 0)
            {
                return true;
            }
            return false;
        }

        public void Register()
        {
            GenericRepository gr = new GenericRepository(new EntityRepository());
            gr.Add<Account>(account);
            gr.SaveChanges();
        }

        public Account GetAccount()
        {
            return this.account;
        }

        public Permissions GetPermissions(Event e)
        {
            Permissions mask = Permissions.EmptyMask;
            bool isOwner = false;
            bool isFriend = false;
            bool isFriendOfFriend = false;

            if (e.Organizers != null)
            {
                isOwner = e.Organizers.Any(x => x.Id == this.account.Id);
                foreach (var x in e.Organizers)
                {
                    isFriend |= this.IsFriend(x);
                    isFriendOfFriend |= this.IsFriendOfFriend(x);
                }
            }

            if (isOwner)
            {
                mask |= Permissions.Copy;
                mask |= Permissions.Delete;
                mask |= Permissions.Edit;
                mask |= Permissions.Modify;
            }

            
            switch (e.Privacy)
            {
                case DataAccess.Enums.Privacy.Public:
                    mask |= Permissions.View;
                    mask |= Permissions.Share;
                    mask |= Permissions.Accept;
                    break;
                case DataAccess.Enums.Privacy.Friends:
                    if (e.Organizers != null)
                    {
                        foreach (var x in e.Organizers)
                        {
                            isFriend |= this.IsFriend(x);
                        }
                    }
                    if (isFriend || isOwner)
                    {
                        mask |= Permissions.View;
                        mask |= Permissions.Accept;
                    }
                    break;
                case DataAccess.Enums.Privacy.FriendsOfFriend:
                    if (e.Organizers != null)
                    {
                        foreach (var x in e.Organizers)
                        {
                            isFriend |= this.IsFriend(x);
                            isFriendOfFriend |= this.IsFriendOfFriend(x);
                        }
                    }
                    if (isFriendOfFriend || isFriend || isOwner)
                    {
                        mask |= Permissions.View;
                        mask |= Permissions.Accept;
                    }
                    break;
                case DataAccess.Enums.Privacy.Private:
                    if (isOwner)
                    {
                        mask |= Permissions.View;
                        mask |= Permissions.Accept;
                    }
                    break;
            }
            mask |= Permissions.View;
            return mask;
        }

        public bool IsFriend(Account ab)
        {
            return Friends.Any(a => a == ab.UserId);
        }

        public bool IsFriendOfFriend(Account ab)
        {
            if (String.IsNullOrEmpty(ab.Friends))
            {
                return false;
            }
            string[] friends = ab.Friends.Split(new char[] { ';' });
            var result = from a in this.Friends
                         where friends.Any(c => c == a)
                         select a;

            return (result.Count() > 0);
        }

        public bool IsInvited(ICollection<Invitation> e)
        {
            var result = from a in e
                         where a.Recipients.Any(b => b.UserId == this.account.UserId)
                         select a;

            return (result.Count() > 0);
        }

        public Permissions GetPermissions(Activity e)
        {
            Permissions mask = Permissions.EmptyMask;
            bool isOwner = false;
            bool isFriend = false;
            bool isFriendOfFriend = false;
            bool isInvited = false;

            if (e.Organizer != null)
            {
                isOwner = e.Organizer.Id == this.account.Id;
                if (!isOwner)
                {
                    isFriend = IsFriend(e.Organizer);
                    isFriendOfFriend = IsFriendOfFriend(e.Organizer);
                    isInvited = IsInvited(e.Invites);
                }
            }

            if (isOwner)
            {
                mask |= Permissions.Copy;
                mask |= Permissions.Delete;
                mask |= Permissions.Edit;
                mask |= Permissions.Modify;
                mask |= Permissions.InviteGuests;
            }
            
            if (isInvited)
            {
                mask |= Permissions.Accept;
                mask |= Permissions.View;
                mask |= Permissions.Copy;
            }

            // for activities, privacy means who can invite people to the activity.
            switch (e.Privacy)
            {
                case DataAccess.Enums.Privacy.Public:
                    mask |= Permissions.View;
                    mask |= Permissions.Share;
                    mask |= Permissions.Accept;
                    mask |= Permissions.InviteGuests;
                    break;
                case DataAccess.Enums.Privacy.Friends:
                    if (isFriend && isInvited)
                    {
                        mask |= Permissions.InviteGuests;
                    }

                    break;
                case DataAccess.Enums.Privacy.FriendsOfFriend:
                    if ((isFriendOfFriend || isFriend) && isInvited)
                    {
                        mask |= Permissions.InviteGuests;
                    }
                    break;
                case DataAccess.Enums.Privacy.Private:
                    break;
            }
            if (mask != Permissions.EmptyMask)
            {
                mask |= Permissions.View;
            }
            return mask;
        }

        public Permissions GetPermissions(InvitationResponse e)
        {
            Permissions mask = Permissions.EmptyMask;
            bool isOwner = false;
            bool isActivtyOwner = false;

            if (isOwner || isActivtyOwner)
            {
                mask |= Permissions.Delete;
            }

            return mask;
        }

        public Permissions GetPermissions(Invitation e)
        {
            Permissions mask = Permissions.EmptyMask;
            bool isOwner = e.Recipients.Any(x => x.Id == this.account.Id);
            bool isActivtyOwner = e.Activity.Organizer.Id == this.account.Id;

            if (isOwner || isActivtyOwner)
            {
                mask |= Permissions.Delete;
            }
            return Permissions.EmptyMask;
        }
    }
}
