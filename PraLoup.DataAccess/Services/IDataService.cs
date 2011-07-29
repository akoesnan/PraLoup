using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Validators;
namespace PraLoup.DataAccess.Services
{
    public interface IDataService
    {
        EntityDataService<Account, AccountValidator> Account { get; }
        EntityDataService<Business, BusinessValidator> Business { get; }
        EntityDataService<Promotion, PromotionValidator> Promotion { get; }
        EntityDataService<PromotionInstance, PromotionInstanceValidator> PromotionInstance { get; }
        EntityDataService<Event, EventValidator> Event { get; }        
        EntityDataService<Deal, OfferValidator> Offer { get; }
        EntityDataService<BusinessUser, BusinessUserValidator> BusinessUser { get; }
        EntityDataService<UserGroup, UserGroupValidator> UserGroup { get; }
       
        EntityDataService<Connection, ConnectionValidator> Connection { get; }        
    }
}
