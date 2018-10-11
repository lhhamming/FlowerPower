using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlowerPower.Models;

namespace FlowerPower.Controllers
{
    public class artikelsController : Controller
    {
        private DB_A3D6D6_FlowerPowerLuukEntities2 db = new DB_A3D6D6_FlowerPowerLuukEntities2();

        // GET: artikels
        public ActionResult Index()
        {
            return View(db.artikels.ToList());
        }

        // GET: artikels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artikel artikel = db.artikels.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            return View(artikel);
        }

        // GET: artikels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: artikels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "artikelid,artikelnaam,prijs,actief")] artikel artikel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                //string pic = System.IO.Path.GetFileName(file.FileName);
                //Vind bestandstype extension
                string type = System.IO.Path.GetExtension(file.FileName);
                //Maak unieke filenaam
                string pic = string.Format(@"{0}{1}", DateTime.Now.Ticks, type);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/artikels"), pic);
                // file is uploaded
                file.SaveAs(path);

                // Sla image naam op in db record
                artikel.image = pic;

            }

            

            if (ModelState.IsValid)
            {
                db.artikels.Add(artikel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artikel);
        }

        // GET: artikels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artikel artikel = db.artikels.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            return View(artikel);
        }

        // POST: artikels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "artikelid,artikelnaam,prijs,actief,image")] artikel artikel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artikel);
        }

        // GET: artikels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artikel artikel = db.artikels.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            return View(artikel);
        }

        // POST: artikels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            artikel artikel = db.artikels.Find(id);
            db.artikels.Remove(artikel);
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
