using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraLoup.BusinessLogic;

namespace PraLoup.WebApp.Models
{
    public class BaseModel
    {
        public bool CanView { get { return this.Permissions.HasFlag(Permissions.View); } }
        public bool CanEdit { get { return this.Permissions.HasFlag(Permissions.Edit); } }
        public bool CanDelete { get { return this.Permissions.HasFlag(Permissions.Delete); } }

        public Permissions Permissions;

        public BaseModel(Permissions permissions)
        {
            this.Permissions = permissions;
        }

    }
}