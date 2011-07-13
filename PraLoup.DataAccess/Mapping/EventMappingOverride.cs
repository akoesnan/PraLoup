using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;
using FluentNHibernate.Automapping.Alterations;

namespace PraLoup.DataAccess.Mapping
{
    public class EventMappingOverride : IAutoMappingOverride<Event>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Event> mapping)
        {
            //mapping
        }
    }

    public class VenueMappingOverride : IAutoMappingOverride<Venue>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Venue> mapping)
        {
            mapping.IgnoreProperty(x => x.DisplayedName);
        }
    }
}
