using PraLoup.DataAccess.Entities;
using PraLoup.BusinessLogic;

namespace PraLoup.WebApp.Models
{
    public class EventModel : BaseModel
    {
        public Event Event;

        public EventModel(Event e, Permissions p)
            : base(p)
        {
            this.Event = e;
        }
    }
}