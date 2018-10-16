using FlowerPower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FlowerPower.Controllers
{
    public class ActivityController : Controller
    {
        private DB_A3D6D6_FlowerPowerLuukEntities db = new DB_A3D6D6_FlowerPowerLuukEntities();

        // GET: Activity
        public ActionResult Index()
        {
            return View(db.medewerkers.ToList());
        }

        public ActionResult Deactivate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medewerker medewerker = db.medewerkers.Find(id);
            if (medewerker == null)
            {
                return HttpNotFound();
            }
            return View(medewerker);
        }

        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        // POST: Activity/Deactivate/5
        public ActionResult DeactivateConfirm(int? id)
        {

            medewerker medewerker = db.medewerkers.Find(id);

            medewerker.actief = false;
            db.SaveChanges();

            return RedirectToAction("Index");

        }


        // GET: Activity/Activate/5
        public ActionResult Activate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medewerker medewerker = db.medewerkers.Find(id);
            if (medewerker == null)
            {
                return HttpNotFound();
            }
            return View(medewerker);
        }

        
        [HttpPost, ActionName("Activate")]
        [ValidateAntiForgeryToken]
        // POST: Activity/Activate/medewerker
        public ActionResult ActivateConfirm(int? id)
        {
            medewerker medewerker = db.medewerkers.Find(id);

            medewerker.actief = true;
            db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}