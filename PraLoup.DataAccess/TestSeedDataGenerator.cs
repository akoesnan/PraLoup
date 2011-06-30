using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataAccess
{
    public class TestSeedDataGenerator : DropCreateDatabaseAlways<EntityRepository>
    {
        #region ISeedDataGenerator Members

        GenericRepository Repository { get; set; }

        protected override void Seed(EntityRepository repository)
        {
            this.Repository = new GenericRepository(repository);

            SetupAccountsData();
            SetupEventData();
            SetupActivityData();
            SetupMetroData();

            //SetupOffersData();
        }

        private void SetupMetroData()
        {
            var metros = new List<MetroArea>() {
                new MetroArea("Seattle", "Washington", "United States"),
                new MetroArea("Portland", "Oregon", "United States")
            };
            metros.ForEach(m => Repository.Add<MetroArea>(m));
            Repository.SaveChanges();
        }
        #endregion

        /// <summary>
        /// Create seed data for users 
        /// </summary>
        private void SetupAccountsData()
        {
            var accounts = new List<Account>() {
                GetAccount("Jhony", "Depp"),
                GetAccount("Bill", "Finger"),
                GetAccount("Martin", "Nordell"),
                GetAccount("John", "Broome"),
                GetAccount("Angela", "Bassett"),                
                GetAccount("Peggy", "Justice"),    
                GetAccount("Andhy", "Koesnandar"),
                GetAccount("Andrew", "Smith")
            };
            accounts.ForEach(s => Repository.Add<Account>(s));
            Repository.SaveChanges();
        }

        /// <summary>
        /// Create seed data for events 
        /// </summary>
        private void SetupEventData()
        {
            var events = new List<Event>() {
                
                GetThingsToDo("Broadway Sunday Farmers Market", "Seattle Central Community College"),
                GetThingsToDo("Queen Anne Farmers Market", "Queen Anne Farmers Market"),
                GetThingsToDo("Madrona Farmers Market", "Madrona Farmers Market"),

                GetThingsToDo("Adele", "Paramount Theatre"),
                GetThingsToDo("Guys & Dolls", "5th Avenue Theatre"),                
                GetThingsToDo("Bruno Mars and Janelle Monae: Hooligans in Wondaland Tour", "WaMu Theater"),
                
                GetThingsToDo("Seattle Sounders FC vs. Vancouver Whitecaps", "Qwest Field"),
                GetThingsToDo("Seattle Sounders FC vs. New York Red Bulls", "Qwest Field"),
                GetThingsToDo("Seattle Sounders FC vs. New England Revolution", "Qwest Field"),
                GetThingsToDo("Seattle Sounders FC vs. Colorado Rapids", "Qwest Field"),

                GetThingsToDo("Baltimore Orioles @ Seattle Mariners", "Safeco Field"),
                GetThingsToDo("Tampa Bay Rays @ Seattle Mariners", "Safeco Field"),
                GetThingsToDo("Los Angeles Angels @ Seattle Mariners", "Safeco Field"),
                GetThingsToDo("Philadelphia Phillies @ Seattle Mariners", "Safeco Field"),
                GetThingsToDo("Seattle Mariners vs. Florida Marlins", "Safeco Field"),
            };
            events.ForEach(e => Repository.Add<Event>(e));
            Repository.SaveChanges();
        }

        /// <summary>
        /// Create seed data for user invite 
        /// </summary>
        private void SetupActivityData()
        {
            var activities = new List<Activity>() {
                GetActivity(GetUserName("Johny","Depp"), "Adele", new string[] {GetUserName("Bill", "Finger"), GetUserName("John", "Broome")}),
                GetActivity(GetUserName("Angela", "Bassett"), "Seattle Sounders FC vs. New England Revolution", new string[] {GetUserName("Peggy", "Justice"), GetUserName("Angela", "Bassett")})                
            };            

            activities.ForEach(a =>
            {
                foreach (var i in a.Invites)
                {
                    Repository.Add<Invitation>(i);
                }
                Repository.Add<Activity>(a);              
            }
            );
            Repository.SaveChanges();
        }

        private Activity GetActivity(string hostUserName, string evtName, string[] invitesUserName)
        {
            var organizerAcct = Repository.FirstOrDefault<Account>(a => hostUserName.Equals(a.UserName, StringComparison.InvariantCultureIgnoreCase));
            var evt = Repository.FirstOrDefault<Event>(e => evtName.Equals(e.Name, StringComparison.InvariantCultureIgnoreCase) == true);
            var inviteAccts = Repository.Where<Account>(a => invitesUserName.Any(i => i.Equals(a.UserName, StringComparison.InvariantCultureIgnoreCase) == true));
            if (organizerAcct != null && evt != null)
            {
                var activity = new Activity()
                {
                    Event = evt,
                    Organizer = organizerAcct,
                };
                var invite = new Invitation(organizerAcct, inviteAccts, activity, String.Format("Lets Go to {0}", evtName));
                activity.Invites.Add(invite);
                return activity;
            }
            // TODO: what to do when I pass in incorret stuff?
            return null;
        }

        private void SetupOffersData(EntityRepository repository)
        {

        }

        private Account GetAccount(string firstName, string lastName)
        {
            var a = new Account()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = GetUserName(firstName, lastName)
            };
            a.Email = String.Format("{0}@example.com", a.UserName);
            a.Address = GetRandomAddress();
            return a;
        }

        private string GetUserName(string firstName, string lastName)
        {
            return firstName.First() + lastName;
        }

        private Address GetRandomAddress()
        {
            return new Address()
            {
                StreetLine1 = "",
                StreetLine2 = "",
                City = "Seattle",
                State = "WA",
                Country = "United States"
            };
        }

        private Event GetThingsToDo(string eventName, string venueName)
        {
            var rand = new Random(eventName.GetHashCode());
            var e = new Event()
            {
                Name = eventName,
                Description = String.Format("Join us to the exciting {0}.", eventName),
                StartDateTime = DateTime.Today.AddDays(rand.Next(5, 30)).AddHours(15),
                EndDateTime = DateTime.Today.AddDays(rand.Next(5, 30)).AddHours(18),
                Price = 50,
                Value = 50,
                Source = "Seed",
                Privacy = Enums.Privacy.Public,
                Venue = GetVenue(venueName)
            };
            return e;
        }

        private Venue GetVenue(string venueName)
        {
            var v = new Venue()
            {
                Name = venueName,
                StreetLine1 = "",
                StreetLine2 = "",
                City = "Seattle",
                State = "WA",
                Country = "United States"
            };
            return v;
        }
    }
}
