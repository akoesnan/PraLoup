using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Data;

namespace PraLoup.WebApp.Areas.Admin.Controllers
{
    /// <summary>
    /// This is controller for the Metro Area
    /// This is used for us to add / remove supported metro area from the system    
    /// </summary>
    public class MetroAreaController : Controller
    {
        IRepository db { get; set; }

        public MetroAreaController(IRepository repository) {
            this.db = repository;
        }

        // TODO: need to add autorization for admin
        // GET: /MetroArea/
        public ActionResult Index()
        {
            var entities = db.GetAll<MetroArea>();
            return View(entities);
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
        public ActionResult Create(MetroArea m)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.SaveOrUpdate(m);
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
            var m = db.Find<MetroArea>(id);
            if (m != null)
            {
                return View(m);
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
