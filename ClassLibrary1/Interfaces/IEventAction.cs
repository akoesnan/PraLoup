using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Entities;

namespace PraLoup.Plugins
{
    public interface IEventAction
    {
        Activity CreateActivityFromExistingEvent(Activity activity);
    
        Activity CreateActivityFromNotExistingEvent(Activity activity);

        IEnumerable<Invitation> Invite(Activity actv, IEnumerable<Invitation> invitations);
    }
}
