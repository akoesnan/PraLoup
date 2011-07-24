using System;

namespace PraLoup.DataAccess.Entities
{
    public class FacebookLogon
    {
        public virtual long FacebookId { get; set; }
        public virtual string AccessToken { get; set; }
        public virtual DateTime Expires { get; set; }

        public override bool Equals(object obj)
        {
            var fl = obj as FacebookLogon;
            return fl.FacebookId == this.FacebookId && fl.AccessToken == this.AccessToken && fl.Expires == this.Expires;
        }

        public override int GetHashCode()
        {
            return this.FacebookId.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Id:{0} AccessToken:{1} Expires:{2}", this.FacebookId, this.AccessToken, this.Expires);
        }
    }
}
