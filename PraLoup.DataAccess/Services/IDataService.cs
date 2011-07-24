using PraLoup.DataAccess.Entities;
namespace PraLoup.DataAccess.Services
{
    public interface IDataService
    {
        EntityDataService<Account, PraLoup.DataAccess.Validators.AccountValidator> Account { get; }
        
        EntityDataService<Event, PraLoup.DataAccess.Validators.EventValidator> Event { get; }
        EntityDataService<PromotionInstance, PraLoup.DataAccess.Validators.InvitationValidator> Invitation { get; }
        EntityDataService<Deal, PraLoup.DataAccess.Validators.OfferValidator> Offer { get; }
        
        EntityDataService<Connection, PraLoup.DataAccess.Validators.ConnectionValidator> Connection { get; }
        void Commit();
        void Rollback();
    }
}
