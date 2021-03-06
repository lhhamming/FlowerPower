﻿using System;
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
    [Authorize]
    public class bestellingsController : Controller
    {
        private DB_A3D6D6_FlowerPowerLuukEntities db = new DB_A3D6D6_FlowerPowerLuukEntities();

        // GET: bestellings
        public ActionResult Index()
        {
            var bestelling = db.bestellings.Include(b => b.klant).Include(b => b.medewerker).Include(b => b.status).Include(b => b.vestiging);
            return View(bestelling.ToList());
        }

        // GET: bestellings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bestelling bestelling = db.bestellings.Find(id);
            if (bestelling == null)
            {
                return HttpNotFound();
            }
            return View(bestelling);
        }

        // GET: bestellings/Create
        public ActionResult Create()
        {
            ViewBag.klantid = new SelectList(db.klants, "klantid", "voorletters");
            ViewBag.medewerkerid = new SelectList(db.medewerkers, "medewerkerid", "voorletters");
            ViewBag.statusid = new SelectList(db.status, "statusid", "status1");
            ViewBag.vestigingid = new SelectList(db.vestigings, "vestigingsid", "vestigingsnaam");
            return View();
        }

        // POST: bestellings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bestellingid,aantal,statusid,besteldatum,ophaaldatum,bestelregelid,medewerkerid,klantid,vestigingid,totaalprijs")] bestelling bestelling)
        {
            if (ModelState.IsValid)
            {
                db.bestellings.Add(bestelling);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.klantid = new SelectList(db.klants, "klantid", "voorletters", bestelling.klantid);
            ViewBag.medewerkerid = new SelectList(db.medewerkers, "medewerkerid", "voorletters", bestelling.medewerkerid);
            ViewBag.statusid = new SelectList(db.status, "statusid", "status1", bestelling.statusid);
            ViewBag.vestigingid = new SelectList(db.vestigings, "vestigingsid", "vestigingsnaam", bestelling.vestigingid);
            return View(bestelling);
        }

        // GET: bestellings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bestelling bestelling = db.bestellings.Find(id);
            if (bestelling == null)
            {
                return HttpNotFound();
            }
            ViewBag.klantid = new SelectList(db.klants, "klantid", "voorletters", bestelling.klantid);
            ViewBag.medewerkerid = new SelectList(db.medewerkers, "medewerkerid", "voorletters", bestelling.medewerkerid);
            ViewBag.statusid = new SelectList(db.status, "statusid", "status1", bestelling.statusid);
            ViewBag.vestigingid = new SelectList(db.vestigings, "vestigingsid", "vestigingsnaam", bestelling.vestigingid);
            return View(bestelling);
        }

        // POST: bestellings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bestellingid,aantal,statusid,besteldatum,ophaaldatum,bestelregelid,medewerkerid,klantid,vestigingid,totaalprijs")] bestelling bestelling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bestelling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.klantid = new SelectList(db.klants, "klantid", "voorletters", bestelling.klantid);
            ViewBag.medewerkerid = new SelectList(db.medewerkers, "medewerkerid", "voorletters", bestelling.medewerkerid);
            ViewBag.statusid = new SelectList(db.status, "statusid", "status1", bestelling.statusid);
            ViewBag.vestigingid = new SelectList(db.vestigings, "vestigingsid", "vestigingsnaam", bestelling.vestigingid);
            return View(bestelling);
        }

        // GET: bestellings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bestelling bestelling = db.bestellings.Find(id);
            if (bestelling == null)
            {
                return HttpNotFound();
            }
            return View(bestelling);
        }

        // POST: bestellings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bestelling bestelling = db.bestellings.Find(id);
            db.bestellings.Remove(bestelling);
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
