using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Entities;
using PraLoup.DataPurveyor.Service;

namespace PraLoup.WebApp.Models
{
    public class ThingsToDoCityModel
    {
        private IEventService DealService {get;set;}
        private IEventService EventsService {get;set;}
        private IEventService HappyHourService {get;set;}
        
        public IEnumerable<Event> Deals { get; private set; }
        public IEnumerable<Event> FunEvents { get; private set; }
        public IEnumerable<Event> HappyHours { get; private set; }

        public string City { get; private set; }

        public ThingsToDoCityModel(string city) {
            this.City = city;

            // TODO: register this in unity 
            this.DealService = new GrouponService();
            this.EventsService = new EventfulService();
        }

        public void Construct() {

            this.Deals = this.DealService.GetEventData(this.City);
            this.FunEvents = this.EventsService.GetEventData(this.City);
        }
    }
}