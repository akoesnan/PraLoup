using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;


namespace ComingSoon
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Request.Form["email"] != null && false)
            {
                SmtpClient sc = new SmtpClient();
                
                sc.Send("info@popr.ly", "info@popr.ly","User Sign up",Page.Request.Form["id_email"]);

            }
        }
    }
}