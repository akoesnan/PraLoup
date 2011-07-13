using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Tag : BaseEntity
    {
        public Tag() { }

        public Tag(string text)
        {
            this.Text = text;
        }

        public virtual string Text { get; set; }

        public override string ToString()
        {
            return string.Format("id{0} text:{1}", this.Id, this.Text);
        }
    }
}
