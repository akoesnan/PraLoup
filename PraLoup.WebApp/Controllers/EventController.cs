using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraLoup.WebApp.Models;
using System.Data.Entity;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Interfaces;

namespace PraLoup.WebApp.Controllers
{
    public class EventController : Controller
    {               
        IRepository db { get; set; }        

        public EventController(IRepository repository) 
        {
            this.db = repository;
        }

        //
        // GET: /Event/        
        public ActionResult Index()
        {
            var entities = db.GetAll<Event>(); 
            return View(entities);
        }

        //
        // GET: /Event/Details/5
        public ActionResult Details(int id)
        {
            var o = db.Find<Event>(id);
            return View(o);
        }

        //
        // GET: /Event/Create
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Event/Create
        [HttpPost]
        public ActionResult Create(Event e)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(e);
                    db.SaveChanges();
                    
                    // TODO: create a page that says events is added succesfully
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Event/Edit/5
        public ActionResult Edit(int id)
        {
            var e = db.Find<Event>(id);
            if (e != null)
            {
                return View(e);
            }
            else 
            {
                // TODO: what to do when there is no such event
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Event/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Event/Delete/5

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
