using System;
using System.Web.Mvc;

namespace PraLoup.WebApp.Areas.Admin.Controllers
{
    public class PromotionInstanceController : Controller
    {
        //
        // GET: /Business/PromotionInstance/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Business/PromotionInstance/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        //
        // GET: /Business/PromotionInstance/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Business/PromotionInstance/Create

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
        // GET: /Business/PromotionInstance/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Business/PromotionInstance/Edit/5

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
        // GET: /Business/PromotionInstance/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Business/PromotionInstance/Delete/5

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
