using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.DataPurveyor.Client;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Interfaces;

namespace PraLoup.WebApp.Models
{
    public class MetroAreaModel
    {
        public IRepository Repository { get; private set; } 
        IEnumerable<MetroArea> supportedMetros;

        public MetroAreaModel(IRepository repository)
        {
            this.Repository = repository;
        }

        public IEnumerable<MetroArea> SupportedMetros
        {
            get
            {
                if (this.supportedMetros == null)
                {
                    this.supportedMetros = Repository.GetAll<MetroArea>().ToList();
                }
                return this.supportedMetros;
            }
        }        

        public bool IsSupportedMetro(string city) {
            if (String.IsNullOrEmpty(city))
            {
                return false;
            }
            else 
            {
                return this.SupportedMetros.Any(m => String.Equals(m.City, city, StringComparison.InvariantCultureIgnoreCase));
            }
        }
    }
}