using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.DataPurveyor.Service;
using PraLoup.DataAccess;

namespace PraLoup.WebApp.Models
{
    public class MetroAreaModel
    {
        GenericRepository db = new GenericRepository(new EntityRepository());

        public IEnumerable<MetroArea> SupportedMetros { get; set; }

        public void Construct()
        {
            this.SupportedMetros = db.GetAll<MetroArea>();
        }
    }
}