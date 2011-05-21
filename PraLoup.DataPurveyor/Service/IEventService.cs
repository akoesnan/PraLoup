using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataPurveyor.Service
{
    interface IEventService
    {
        IEnumerable<Event> GetEventData(string city);
    }
}
