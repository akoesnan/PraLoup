using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;

namespace PraLoup.WebApp.Models
{
    public class PromotionViewModel
    {
        public Event Event;
        
        public Promotion Promotion;
        public Deal Deal;
        public Account Sender;
        public string Message;
        public DateTime InviteDate;
        public StatusType Status;
        public AccountListJson Invited;
        public string InviteMessage;

    }
}