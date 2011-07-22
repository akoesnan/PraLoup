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

    public class VenueMappingOverride : IAutoMappingOverride<Venue>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Venue> mapping)
        {
            mapping.IgnoreProperty(x => x.DisplayedName);
        }
    }

    public class ActivityMappingOverride : IAutoMappingOverride<Activity>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Activity> mapping)
        {
            mapping.References(x => x.Event).Not.Nullable().Index("EventId");
            mapping.References(x => x.Organizer).Not.Nullable();
            mapping.IgnoreProperty(x => x.Permission);
            mapping.IgnoreProperty(x => x.ConnectionType);
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

    public class InvitationMappingOverride : IAutoMappingOverride<Invitation>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Invitation> mapping)
        {
            mapping.IgnoreProperty(x => x.Permission);
            mapping.IgnoreProperty(x => x.ConnectionType);
        }
    }

    public class InvitationResponseMappingOverride : IAutoMappingOverride<InvitationResponse>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<InvitationResponse> mapping)
        {
            mapping.IgnoreProperty(x => x.Permission);
            mapping.IgnoreProperty(x => x.ConnectionType);
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
