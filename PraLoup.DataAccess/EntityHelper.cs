using System;
using System.Linq;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataAccess
{
    public static class EntityHelper
    {
        static Random MyRandom = new Random(123123);

        public static Event GetEvent(string eventName, string venueName)
        {
            var rand = new Random(eventName.GetHashCode());
            var e = new Event()
            {
                Name = eventName,
                Description = String.Format("Join us to the exciting {0}.", eventName),
                StartDateTime = DateTime.Today.AddDays(rand.Next(1, 3)).AddHours(15),
                EndDateTime = DateTime.Today.AddDays(rand.Next(3, 5)).AddHours(18),
                Price = 50,
                Value = 50,
                Source = "Seed",
                Privacy = PraLoup.DataAccess.Enums.Privacy.Public,
                Venue = GetVenue(venueName)
            };
            return e;
        }

        public static Deal GetDeal(string description, int available)
        {
            var d = new Deal()
            {
                Available = available,
                Taken = 0,
                CurrentValue = 5,
                Description = "description",
                EndDateTime = DateTime.Now.AddDays(5),
                StartDateTime = DateTime.Now,
            };
            return d;
        }

        public static Venue GetVenue(string venueName)
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


        public static Account GetAccount(string firstName, string lastName)
        {
            var a = new Account()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = GetUserName(firstName, lastName)
            };
            a.FacebookLogon = GetFacebookLogon();
            a.Email = String.Format("{0}@example.com", a.UserName);
            a.Address = GetRandomAddress();
            return a;
        }

        public static string GetUserName(string firstName, string lastName)
        {
            return firstName.First() + lastName;
        }

        public static FacebookLogon GetFacebookLogon()
        {
            return new FacebookLogon()
            {
                AccessToken = "abc",
                Expires = DateTime.Now.AddDays(10),
                FacebookId = MyRandom.Next(10000, 1000000)
            };
        }

        public static Address GetRandomAddress()
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
    }
}
