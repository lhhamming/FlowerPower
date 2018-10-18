using FlowerPower.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FlowerPower.Controllers
{
    public class AssignFilController : Controller
    {

        private DB_A3D6D6_FlowerPowerLuukEntities db = new DB_A3D6D6_FlowerPowerLuukEntities();

        // GET: AssignFil : Show all medewerkers with vestiging
        public ActionResult Index()
        {
            
            
            //var MederwerkerList = db.medewerkers.Where(m => m.vestigingsid == null);
            return View(db.medewerkers.ToList());

        }

        // GET: AssignFil/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.vestigingsid = new SelectList(db.vestigings, "vestigingsid", "vestigingsnaam", medewerker.vestigingsid);
            ViewBag.AspNetUserID = new SelectList(db.AspNetUsers, "Id", "Email", medewerker.AspNetUserID);
            return View(medewerker);
        }

        // POST: AssignFil/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "medewerkerid,voorletters,tussenvoegsels,achternaam,vestigingsid,actief, AspNetUserID")] medewerker medewerker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medewerker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.vestigingsid = new SelectList(db.vestigings, "vestigingsid", "vestigingsnaam", medewerker.vestigingsid);
            ViewBag.AspNetUserID = new SelectList(db.AspNetUsers, "Id", "Email", medewerker.AspNetUserID);
            return View(medewerker);
        }

    }
}