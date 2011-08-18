
using System;
namespace PraLoup.DataAccess.Entities
{
    public class HoursOfOperation : BaseEntity
    {
        public HoursOfOperation() { }

        public HoursOfOperation(int day, TimeSpan open, TimeSpan close)
        {
            this.Day = day;
            this.OpenTime = open;
            this.CloseTime = close;
        }

        public virtual int Day { get; set; }
        public virtual TimeSpan OpenTime { get; set; }
        public virtual TimeSpan CloseTime { get; set; }

        
    }
}
