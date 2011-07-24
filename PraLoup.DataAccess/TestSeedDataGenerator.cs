using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.DataAccess
{
    public class TestSeedDataGenerator
    {
        private IDataService DataService;
        private ILogger Log;

        public TestSeedDataGenerator(IDataService dataService, ILogger log)
        {
            this.DataService = dataService;
            this.Log = log;
        }

        public void Seed()
        {
            SetupAccountsData();
            SetupEventData();
            //SetupOffersData();

            this.DataService.Commit();
        }

        /// <summary>
        /// Create seed data for users 
        /// </summary>
        private void SetupAccountsData()
        {
            var accounts = new List<Account>() {
                EntityHelper.GetAccount("Jhony", "Depp"),
                EntityHelper.GetAccount("Bill", "Finger"),
                EntityHelper.GetAccount("Martin", "Nordell"),
                EntityHelper.GetAccount("John", "Broome"),
                EntityHelper.GetAccount("Angela", "Bassett"),                
                EntityHelper.GetAccount("Peggy", "Justice"),    
                EntityHelper.GetAccount("Andhy", "Koesnandar"),
                EntityHelper.GetAccount("Andrew", "Smith")
            };
            IEnumerable<string> brokenRules = null;
            accounts.ForEach(s => this.DataService.Account.SaveOrUpdate(s, out brokenRules));
            if (brokenRules != null)
                this.Log.Debug("brokenRules for Account {0}", String.Join(",", brokenRules));
        }

        /// <summary>
        /// Create seed data for events 
        /// </summary>
        private void SetupEventData()
        {
            var events = new List<Event>() {
                
                EntityHelper.GetEvent("Broadway Sunday Farmers Market", "Seattle Central Community College"),
                EntityHelper.GetEvent("Queen Anne Farmers Market", "Queen Anne Farmers Market"),
                EntityHelper.GetEvent("Madrona Farmers Market", "Madrona Farmers Market"),

                EntityHelper.GetEvent("Adele", "Paramount Theatre"),
                EntityHelper.GetEvent("Guys & Dolls", "5th Avenue Theatre"),                
                EntityHelper.GetEvent("Bruno Mars and Janelle Monae: Hooligans in Wondaland Tour", "WaMu Theater"),
                
                EntityHelper.GetEvent("Seattle Sounders FC vs. Vancouver Whitecaps", "Qwest Field"),
                EntityHelper.GetEvent("Seattle Sounders FC vs. New York Red Bulls", "Qwest Field"),
                EntityHelper.GetEvent("Seattle Sounders FC vs. New England Revolution", "Qwest Field"),
                EntityHelper.GetEvent("Seattle Sounders FC vs. Colorado Rapids", "Qwest Field"),

                EntityHelper.GetEvent("Baltimore Orioles @ Seattle Mariners", "Safeco Field"),
                EntityHelper.GetEvent("Tampa Bay Rays @ Seattle Mariners", "Safeco Field"),
                EntityHelper.GetEvent("Los Angeles Angels @ Seattle Mariners", "Safeco Field"),
                EntityHelper.GetEvent("Philadelphia Phillies @ Seattle Mariners", "Safeco Field"),
                EntityHelper.GetEvent("Seattle Mariners vs. Florida Marlins", "Safeco Field"),
            };
            IEnumerable<string> brokenRules = null;
            events.ForEach(e => this.DataService.Event.SaveOrUpdate(e, out brokenRules));
            if (brokenRules != null)
                this.Log.Debug("brokenRules for Event {0}", String.Join(",", brokenRules));

        }
    }
}
