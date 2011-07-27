using PraLoup.DataAccess.Entities;
namespace PraLoup.DataAccess.Services
{
    public interface IDataService
    {
        EntityDataService<Account, PraLoup.DataAccess.Validators.AccountValidator> Account { get; }
        EntityDataService<Business, PraLoup.DataAccess.Validators.BusinessValidator> Business { get; }
        EntityDataService<Promotion, PraLoup.DataAccess.Validators.PromotionValidator> Promotion { get; }
        EntityDataService<PromotionInstance, PraLoup.DataAccess.Validators.PromotionInstanceValidator> PromotionInstance { get; }
        EntityDataService<Event, PraLoup.DataAccess.Validators.EventValidator> Event { get; }        
        EntityDataService<Deal, PraLoup.DataAccess.Validators.OfferValidator> Offer { get; }
        EntityDataService<BusinessUser, PraLoup.DataAccess.Validators.BusinessUserValidator> BusinessUsers { get; }

        EntityDataService<Connection, PraLoup.DataAccess.Validators.ConnectionValidator> Connection { get; }        
    }
}
