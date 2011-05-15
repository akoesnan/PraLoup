using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess;

namespace PraLoup.WebApp.Controllers
{
    public class OfferController : Controller
    {
        //
        // GET: /Offer/
        GenericRepository repository = new GenericRepository(new EntityRepository());

        public ActionResult Index()
        {
            var entities = repository.GetAll<Offer>();
            return View(entities);
        }

        //
        // GET: /Offer/Details/5

        public ActionResult Details(int id)
        {
            var offer = repository.Find<Offer>(id);
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
