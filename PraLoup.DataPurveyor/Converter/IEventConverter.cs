using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataPurveyor.Converter
{
    public interface IEventConverter
    {
        Event GetEventObject(dynamic ev); 
    }
}
