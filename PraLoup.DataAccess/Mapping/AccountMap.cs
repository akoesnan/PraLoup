using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;
using FluentNHibernate.Mapping;

namespace PraLoup.DataAccess.Mapping
{
    //public class AccountMap : ClassMap<Account>
    //{
    //    public AccountMap()
    //    {
    //        Table("Accounts");
    //        Id(a => a.Id);
    //        Map(a => a.FirstName);
    //        Map(a => a.LastName);
    //        Map(a => a.PhoneNumber);            
    //        Map(a => a.Email);            
    //        Map(a => a.Friends);
    //        Map(a => a.TwitterId);
    //        References(a => a.UserId).Column("FacebookId");
    //        References(a => a.Address).Column("AddressId").Cascade.All();            
    //    }
    //}

    //public class ActivityMap : ClassMap<Activity>
    //{
    //    public ActivityMap() {
    //        Table("Activities");
    //        Id(a => a.Id);
    //    }
    //}

    //public class AddressMap : ClassMap<Address>
    //{
    //    public AddressMap()
    //    {
    //        Table("Addresses");
    //        Id(a => a.Id);
    //    }
    //}

    //public class EventMap : ClassMap<Event>
    //{
    //    public EventMap()
    //    {
    //        Table("Events");
    //        Id(e => e.Id);
    //    }
    //}

    //public class CommentMap : ClassMap<Comment>
    //{
    //    public CommentMap()
    //    {
    //        Table("Comments");
    //        Id(c => c.Id);
    //    }
    //}

    //public class FacebookLogonMap : ClassMap<FacebookLogon>
    //{
    //    public FacebookLogonMap()
    //    {
    //        Table("FacebookLogons");
    //        Id(f => f.Id);
    //    }
    //}

    //public class InvitationMap : ClassMap<Invitation>
    //{
    //    public InvitationMap()
    //    {
    //        Table("Invitations");
    //        Id(i => i.Id);
    //    }
    //}

    //public class MetroAreaMap : ClassMap<MetroArea>
    //{
    //    public MetroAreaMap()
    //    {
    //        Table("MetroAreas");
    //        Id(m => m.Id);
    //    }
    //}
    
    //public class OfferMap : ClassMap<Offer>
    //{
    //    public OfferMap()
    //    {
    //        Table("Offers");
    //        Id(o => o.Id);
    //    }
    //}

    //public class VenueMap : ClassMap<Venue>
    //{
    //    public VenueMap()
    //    {
    //        Table("Venues");
    //        Id(v => v.Id);
    //    }
    //}

}
