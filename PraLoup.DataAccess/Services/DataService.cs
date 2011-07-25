using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Validators;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Services
{
    public class DataService : IDataService
    {
        public EntityDataService<Account, AccountValidator> Account { get; private set; }
        public EntityDataService<Event, EventValidator> Event { get; private set; }
        public EntityDataService<PromotionInstance, InvitationValidator> Invitation { get; private set; }
        public EntityDataService<Deal, OfferValidator> Offer { get; private set; }
        public EntityDataService<Connection, ConnectionValidator> Connection { get; private set; }
        public EntityDataService<Promotion, PromotionValidator> Promotions { get; private set; }

        private IUnitOfWork unitOfWork;

        public DataService(EntityDataService<Account, AccountValidator> accountDataService,
            EntityDataService<Event, EventValidator> eventDataService,
            EntityDataService<PromotionInstance, InvitationValidator> invitationDataService,
            EntityDataService<Connection, ConnectionValidator> connectionDataService,
            IUnitOfWork unitOfWork)
        {
            this.Account = accountDataService;
            this.Event = eventDataService;
            this.Invitation = invitationDataService;
            this.Connection = connectionDataService;
            this.unitOfWork = unitOfWork;
        }

        public void Commit()
        {
            this.unitOfWork.Commit();
        }

        public void Rollback()
        {
            this.unitOfWork.Rollback();
        }
    }
}
