using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlowerPower.Models;

namespace FlowerPower.Controllers
{
    public class vestigingsController : Controller
    {
        private DB_A3D6D6_FlowerPowerLuukEntities db = new DB_A3D6D6_FlowerPowerLuukEntities();

        // GET: vestigings
        public ActionResult Index()
        {
            return View(db.vestigings.ToList());
        }

        // GET: vestigings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vestiging vestiging = db.vestigings.Find(id);
            if (vestiging == null)
            {
                return HttpNotFound();
            }
            return View(vestiging);
        }

        // GET: vestigings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: vestigings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vestigingsid,vestigingsnaam,vestigingsadres,vestigingspostcode,vestigingsplaats,telefoonnummer,actief")] vestiging vestiging)
        {
            if (ModelState.IsValid)
            {
                db.vestigings.Add(vestiging);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vestiging);
        }

        // GET: vestigings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vestiging vestiging = db.vestigings.Find(id);
            if (vestiging == null)
            {
                return HttpNotFound();
            }
            return View(vestiging);
        }

        // POST: vestigings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vestigingsid,vestigingsnaam,vestigingsadres,vestigingspostcode,vestigingsplaats,telefoonnummer,actief")] vestiging vestiging)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vestiging).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vestiging);
        }

        // GET: vestigings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vestiging vestiging = db.vestigings.Find(id);
            if (vestiging == null)
            {
                return HttpNotFound();
            }
            return View(vestiging);
        }

        // POST: vestigings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vestiging vestiging = db.vestigings.Find(id);
            db.vestigings.Remove(vestiging);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
