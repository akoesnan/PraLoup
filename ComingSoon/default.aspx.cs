using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;


namespace ComingSoon
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Request.Form["email"] != null && false)
            {
                SmtpClient smtp = new SmtpClient 
                { 
                    Host = "smtp.gmail.com", 
                    Port = 587, 
                    EnableSsl = true, 
                    DeliveryMethod = SmtpDeliveryMethod.Network, 
                    UseDefaultCredentials = false, 
                    Credentials = new NetworkCredential("infopoprly@gmail.com", "P0prlyR0ck") 
                };

                smtp.Send("infopoprly@gmail.com", "infopoprly@gmail.com", "User Sign up","email " + System.Web.HttpUtility.UrlEncode(Page.Request.Form["email"]));

            }
        }
    }
}