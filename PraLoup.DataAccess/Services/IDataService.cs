using PraLoup.DataAccess.Entities;
namespace PraLoup.DataAccess.Services
{
    public interface IDataService
    {
        EntityDataService<Account, PraLoup.DataAccess.Validators.AccountValidator> Account { get; }
        EntityDataService<Activity, PraLoup.DataAccess.Validators.ActivityValidator> Activity { get; }
        EntityDataService<Event, PraLoup.DataAccess.Validators.EventValidator> Event { get; }
        EntityDataService<Invitation, PraLoup.DataAccess.Validators.InvitationValidator> Invitation { get; }
        EntityDataService<Offer, PraLoup.DataAccess.Validators.OfferValidator> Offer { get; }
        EntityDataService<MetroArea, PraLoup.DataAccess.Validators.MetroAreaValidator> MetroArea { get; }
        EntityDataService<Connection, PraLoup.DataAccess.Validators.ConnectionValidator> Connection { get; }
        void Commit();
        void Rollback();
    }
}
