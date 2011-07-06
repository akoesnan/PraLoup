using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataPurveyor.Client
{
    public interface IEventClient
    {
        
        IEnumerable<Event> GetEventData(string city);

        /// <summary>
        /// The filter method to decide if the Event should be included
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        bool IsSelected(Event e);
    }
}
