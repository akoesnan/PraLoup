using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic.Plugins
{
    public interface IActivityAction
    {
        Activity CreateActivityFromExistingEvent(Activity activity);

        Activity CreateActivityFromNotExistingEvent(Activity activity);

        IEnumerable<Invitation> Invite(Activity actv, IEnumerable<Invitation> invitations);
    }
}
