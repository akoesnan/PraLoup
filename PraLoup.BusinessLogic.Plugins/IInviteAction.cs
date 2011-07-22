using System.Collections.Generic;
using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic.Plugins
{
    public interface IInviteAction
    {
        IEnumerable<Invitation> Invite(Activity actv, IEnumerable<Account> invites, string message);

        Invitation Response(Invitation activity);
    }
}
