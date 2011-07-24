using System;
using FluentNHibernate.Automapping;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataAccess.Mapping
{
    public class PraLoupAutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            // specify the criteria that types must meet in order to be mapped
            // any type for which this method returns false will not be mapped.
            return type.Namespace == "PraLoup.DataAccess.Entities";
        }

        public override bool IsComponent(Type type)
        {
            // override this method to specify which types should be treated as components
            // if you have a large list of types, you should consider maintaining a list of them
            // somewhere or using some form of conventional and/or attribute design
            return type == typeof(FacebookLogon)
                || type == typeof(PromotionInstanceStatus);
        }

        public override string SimpleTypeCollectionValueColumn(FluentNHibernate.Member member)
        {
            return base.SimpleTypeCollectionValueColumn(member);
        }
    }
}
