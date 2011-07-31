using System;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;

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
                DealValue = 5,
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
                Country = Country.US
            };
            return v;
        }

        public static Business GetBusiness(string businessName, Category businessCategory)
        {
            var b = new Business()
            {
                Name = businessName,
                Category = businessCategory,
                Phone = "1231231234",
                Url = "http://abc.abc.com",
                Address = GetRandomAddress()
            };
            return b;
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
                State = State.WA.ToString(),
                Country = Country.US
            };
        }

        public static Promotion GetPromo(Business b, Event ev, Deal d)
        {
            return new Promotion(b, ev, d, 100, 5);
        }
    }
}
