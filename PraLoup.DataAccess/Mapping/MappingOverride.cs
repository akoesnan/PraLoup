using FluentNHibernate.Automapping.Alterations;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataAccess.Mapping
{
    public class AccountMappingOverride : IAutoMappingOverride<Account>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Account> mapping)
        {
            mapping.IgnoreProperty(x => x.FacebookFriendIds);
            mapping.HasMany(x => x.Connections);
        }
    }

    public class BusinessMappingOverride : IAutoMappingOverride<Business>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Business> mapping)
        {
            mapping.IgnoreProperty(x => x.FacebookFriendIds);            
            mapping.HasMany(x => x.Connections);
            mapping.HasMany(x => x.HoursOfOperations).Component(x =>
            {
                x.Map(c => c.Day);
                x.Map(c => c.OpenTime);
                x.Map(c => c.CloseTime);
            });
        }
    }

    public class VenueMappingOverride : IAutoMappingOverride<Venue>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Venue> mapping)
        {
            mapping.IgnoreProperty(x => x.DisplayedName);
        }
    }

    public class EventMappingOverride : IAutoMappingOverride<Event>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Event> mapping)
        {
            mapping.IgnoreProperty(x => x.Permission);
            mapping.IgnoreProperty(x => x.ConnectionType);
        }
    }

    public class InvitationMappingOverride : IAutoMappingOverride<PromotionInstance>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<PromotionInstance> mapping)
        {
            mapping.HasMany(x => x.UserRatings);
            mapping.HasMany(x => x.Coupons);
            mapping.HasMany(x => x.ForwardedPromotionInstances);

            mapping.IgnoreProperty(x => x.Permission);
            mapping.IgnoreProperty(x => x.ConnectionType);
        }
    }

    public class PromotionInstanceStatusMappingOverride : IAutoMappingOverride<PromotionInstanceStatus>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<PromotionInstanceStatus> mapping)
        {
            mapping.IgnoreProperty(x => x.Permission);

        }
    }

    public class ConnectionMappingOverride : IAutoMappingOverride<Connection>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Connection> mapping)
        {
            mapping.Map(x => x.MyId).Not.Nullable().Index("MyId");
            mapping.Map(x => x.FriendId).Not.Nullable().Index("FriendId");
        }
    }
}
