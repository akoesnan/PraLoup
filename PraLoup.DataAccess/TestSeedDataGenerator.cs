using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
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
            SetupBusinessData();
            SetupPromotionData();
            SetupPromotionInstanceData();
            //SetupEventData();            
        }

        private void SetupPromotionInstanceData()
        {
            var promotionInstances = new List<PromotionInstance>(){
                new PromotionInstance(accounts["jd"], accounts["bf"], promos[0], promos[0].Deals[0], "happy hours at sunliquors everyone?"),
                new PromotionInstance(accounts["jd"], accounts["mn"], promos[0], promos[0].Deals[0], "happy hours at sunliquors everyone?"),
                new PromotionInstance(accounts["jd"], accounts["jb"], promos[0], promos[0].Deals[0], "happy hours at sunliquors everyone?"),

                new PromotionInstance(accounts["bf"], accounts["jb"], promos[1], promos[1].Deals[0], "happy hours at rockbox everyone?"),
                new PromotionInstance(accounts["bf"], accounts["ab"], promos[1], promos[1].Deals[0], "happy hours at rockbox everyone?"),
                new PromotionInstance(accounts["bf"], accounts["ak"], promos[1], promos[1].Deals[0], "happy hours at rockbox everyone?"),

            };

            IEnumerable<string> brokenRules = null;
            foreach (var a in promotionInstances)
            {
                this.DataService.PromotionInstance.SaveOrUpdate(a, out brokenRules);
                if (brokenRules != null)
                    this.Log.Debug("brokenRules for Account {0}", String.Join(",", brokenRules));
            }
        }

        Dictionary<string, Account> accounts;

        /// <summary>
        /// Create seed data for users 
        /// </summary>
        private void SetupAccountsData()
        {
            accounts = new Dictionary<string, Account>() {
                {"jd",EntityHelper.GetAccount("Jhony", "Depp")},
                {"bf",EntityHelper.GetAccount("Bill", "Finger")},
                {"mn",EntityHelper.GetAccount("Martin", "Nordell")},
                {"jb",EntityHelper.GetAccount("John", "Broome")},
                {"ab", EntityHelper.GetAccount("Angela", "Bassett")},                
                {"pj", EntityHelper.GetAccount("Peggy", "Justice")},    
                {"ak", EntityHelper.GetAccount("Andhy", "Koesnandar")},
                {"as", EntityHelper.GetAccount("Andrew", "Smith")}
            };
            IEnumerable<string> brokenRules = null;
            foreach (var a in accounts.Values)
            {
                this.DataService.Account.SaveOrUpdate(a, out brokenRules);
                if (brokenRules != null)
                    this.Log.Debug("brokenRules for Account {0}", String.Join(",", brokenRules));
            }
        }


        Dictionary<string, Business> businesses;

        private void SetupBusinessData()
        {
            businesses = new Dictionary<string, Business>() {
            {"Sun Liquor", EntityHelper.GetBusiness("Sun Liquor", Category.Nightlife )},
            {"Showbox",EntityHelper.GetBusiness("Showbox at the Market", Category.Nightlife )},
            {"Tavern Law",EntityHelper.GetBusiness("Tavern Law", Category.Nightlife )},
            {"Monsoon Restaurant",EntityHelper.GetBusiness("Monsoon Restaurant", Category.Restaurant )},
            {"Poppy",EntityHelper.GetBusiness("Poppy", Category.Restaurant )},
            {"Rockbox",EntityHelper.GetBusiness("Rockbox", Category.Karaoke)},
            {"Tidbit Bistro & Restaurant",EntityHelper.GetBusiness("Tidbit Bistro & Restaurant", Category.Restaurant )}            

         };
            IEnumerable<string> brokenRules = null;
            foreach (var b in businesses.Values)
            {
                this.DataService.Business.SaveOrUpdate(b, out brokenRules);
                if (brokenRules != null)
                    this.Log.Debug("brokenRules for Business {0}", String.Join(",", brokenRules));
            }
        }
        List<Promotion> promos;

        private void SetupPromotionData()
        {
            var events = new List<Event>() {                
                EntityHelper.GetEvent("shinny happy sundays", "Sun Liquor"),
                EntityHelper.GetEvent("2 for 1 karaoke", "Rockbox"),                
                EntityHelper.GetEvent("free beer pitcher for 3 hours karaoke", "Rockbox"),
            };

            var deals = new List<Deal>() {
                EntityHelper.GetDeal("3 dollars wheels", 1000),
                EntityHelper.GetDeal("$2 dollar karaoke", 1000),
                EntityHelper.GetDeal("3 hours of karaoke gets you get a pitcher of beer free", 1000)
            };

            promos = new List<Promotion>() {
                EntityHelper.GetPromo(businesses["Sun Liquor"], events[0],  deals[0] ),
                EntityHelper.GetPromo(businesses["Rockbox"], events[1],  deals[1] ),
                EntityHelper.GetPromo(businesses["Rockbox"], events[2],  deals[2] ),
            };
            IEnumerable<string> brokenRules = null;
            foreach (var b in promos)
            {
                this.DataService.Promotion.SaveOrUpdate(b, out brokenRules);
                if (brokenRules != null)
                    this.Log.Debug("brokenRules for Promo {0}", String.Join(",", brokenRules));
            }
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
