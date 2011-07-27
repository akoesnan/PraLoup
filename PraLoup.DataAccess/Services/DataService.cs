﻿using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Validators;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Services
{
    public class DataService : IDataService
    {
        public EntityDataService<Account, AccountValidator> Account { get; private set; }
        public EntityDataService<Business, BusinessValidator> Business { get; private set; }
        public EntityDataService<Promotion, PromotionValidator> Promotion { get; private set; }
        public EntityDataService<PromotionInstance, PromotionInstanceValidator> PromotionInstance { get; private set; }
        public EntityDataService<Event, EventValidator> Event { get; private set; }
        public EntityDataService<Deal, OfferValidator> Offer { get; private set; }
        public EntityDataService<Connection, ConnectionValidator> Connection { get; private set; }
        public EntityDataService<Promotion, PromotionValidator> Promotions { get; private set; }
        public EntityDataService<BusinessUser, BusinessUserValidator> BusinessUsers { get; private set; }
        public IUnitOfWork UnitOfWork { get; private set; }

        public DataService(EntityDataService<Account, AccountValidator> accountDataService,
            EntityDataService<Business, BusinessValidator> businessDataService,
            EntityDataService<Promotion, PromotionValidator> promotionDataService,
            EntityDataService<Event, EventValidator> eventDataService,
            EntityDataService<PromotionInstance, PromotionInstanceValidator> promoInstanceDataService,
            EntityDataService<Connection, ConnectionValidator> connectionDataService,
            EntityDataService<BusinessUser, BusinessUserValidator> businessUserDataService,
            IUnitOfWork unitOfWork)
        {
            this.Account = accountDataService;
            this.Business = businessDataService;
            this.Promotion = promotionDataService;
            this.PromotionInstance = promoInstanceDataService;
            this.Event = eventDataService;
            this.Connection = connectionDataService;
            this.UnitOfWork = unitOfWork;
            this.BusinessUsers = businessUserDataService;
        }
    }
}
