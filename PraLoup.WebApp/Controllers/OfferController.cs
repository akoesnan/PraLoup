using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Interfaces;

namespace PraLoup.WebApp.Controllers
{
    public class OfferController : Controller
    {
        IRepository Repository { get; set; }

        public OfferController(IRepository repository) {
            this.Repository = repository;
        } 
        //
        // GET: /Offer/
        

        public ActionResult Index()
        {
            var entities = Repository.GetAll<Offer>();
            return View(entities);
        }

        //
        // GET: /Offer/Details/5

        public ActionResult Details(int id)
        {
            var offer = Repository.Find<Offer>(id);
            return View(offer);
        }

        //
        // GET: /Offer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Offer/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Offer/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Offer/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Offer/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Offer/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
