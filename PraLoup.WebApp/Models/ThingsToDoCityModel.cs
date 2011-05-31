using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PraLoup.DataAccess.Entities;
using PraLoup.DataPurveyor.Attributes;
using PraLoup.DataPurveyor.Service;

namespace PraLoup.WebApp.Models
{
    public class ThingsToDoCityModel
    {
        private IEnumerable<IEventService> DealServices { get; set; }
        private IEnumerable<IEventService> EventsServices { get; set; }
        private IEnumerable<IEventService> HappyHourServices { get; set; }
        private IEnumerable<IEventService> PromotedServices { get; set; }

        public IEnumerable<Event> Deals { get; private set; }
        public IEnumerable<Event> FunEvents { get; private set; }
        public IEnumerable<Event> HappyHours { get; private set; }
        public IEnumerable<Event> PromotedEvents { get; private set; }

        public string City { get; private set; }

        public bool UseMultiThreads { get; set; }

        /// <summary>
        /// Model for things to do in a city
        /// </summary>
        /// <param name="dealsServices"></param>
        /// <param name="eventsServices"></param>
        /// <param name="happyHoursServices"></param>
        public ThingsToDoCityModel([DealsAttributes] IEnumerable<IEventService> dealsServices,
                                   [EventsAttributes]IEnumerable<IEventService> eventsServices,
                                   [HappyHourAttributes] IEnumerable<IEventService> happyHoursServices)
        {
            this.DealServices = dealsServices;
            this.EventsServices = eventsServices;
            this.HappyHourServices = happyHoursServices;
        }

        public void Construct(string city)
        {
            if (UseMultiThreads == true)
            {
                ConstructMultiThreads(city);
            }
            else
            {
                ConstructSingleThread(city);
            }
        }

        private void ConstructMultiThreads(string city)
        {
            this.City = city;

            var getDealsTask = new Task(() => GetDealsData(city));
            var getFunEventsTask = new Task(() => GetFunEventsData(city));
            var getHappyHoursTask = new Task(() => GetHappyHoursData(city));

            getDealsTask.Start();
            getFunEventsTask.Start();
            getHappyHoursTask.Start();
        }

        private void ConstructSingleThread(string city)
        {
            this.City = city;

            GetDealsData(city);
            GetFunEventsData(city);
            GetHappyHoursData(city);
        }

        public void GetDealsData(string city)
        {
            // TODO: make this multi threaded using plinq when there are multiple data purveyor:
            // this.Deals = this.DealServices.AsParallel().SelectMany(s => s.GetEventData(this.City));
            this.Deals = this.DealServices.SelectMany(s => s.GetEventData(city));
        }
        public void GetFunEventsData(string city)
        {
            // TODO: make this multi threaded using plinq when there are multiple data purveyor:
            // this.Deals = this.DealServices.AsParallel().SelectMany(s => s.GetEventData(this.City));
            this.FunEvents = this.EventsServices.SelectMany(s => s.GetEventData(city));
        }

        public void GetHappyHoursData(string city)
        {
            // TODO: make this multi threaded using plinq when there are multiple data purveyor:
            // this.Deals = this.DealServices.AsParallel().SelectMany(s => s.GetEventData(this.City));
            this.HappyHours = this.HappyHourServices.SelectMany(s => s.GetEventData(city));
        }
    }
}