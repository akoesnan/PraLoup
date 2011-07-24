﻿using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Validators;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Services
{
    public class DataService : IDataService
    {
        public EntityDataService<Account, AccountValidator> Account { get; private set; }
        public EntityDataService<Event, EventValidator> Event { get; private set; }
        public EntityDataService<Activity, ActivityValidator> Activity { get; private set; }
        public EntityDataService<Invitation, InvitationValidator> Invitation { get; private set; }
        public EntityDataService<Offer, OfferValidator> Offer { get; private set; }
        public EntityDataService<MetroArea, MetroAreaValidator> MetroArea { get; private set; }
        public EntityDataService<Connection, ConnectionValidator> Connection { get; private set; }

        private IUnitOfWork unitOfWork;

        public DataService(EntityDataService<Account, AccountValidator> accountDataService,
            EntityDataService<Event, EventValidator> eventDataService,
            EntityDataService<Activity, ActivityValidator> activityDataService,
            EntityDataService<Invitation, InvitationValidator> invitationDataService,
            EntityDataService<MetroArea, MetroAreaValidator> metroAreaDataService,
            EntityDataService<Connection, ConnectionValidator> connectionDataService,
            IUnitOfWork unitOfWork)
        {
            this.Account = accountDataService;
            this.Event = eventDataService;
            this.Activity = activityDataService;
            this.Invitation = invitationDataService;
            this.MetroArea = metroAreaDataService;
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