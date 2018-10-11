﻿using FlowerPower.Models;
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

        private DB_A3D6D6_FlowerPowerLuukEntities1 db = new DB_A3D6D6_FlowerPowerLuukEntities1();

        // GET: AssignFil
        public ActionResult Index()
        {

            var MederwerkerList = db.medewerker.Where(m => m.vestigingid == 0);



            return View(MederwerkerList);
        }

        // GET: AssignFil/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medewerker medewerker = db.medewerker.Find(id);
            if (medewerker == null)
            {
                return HttpNotFound();
            }
            return View(medewerker);
        }

        // POST: AssignFil/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vestigingid")] artikel artikel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artikel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Assign(int id, int vestid)
        //{

        //    medewerker BewerkteMw = db.medewerker.FirstOrDefault(x => x.medewerkerid == id);
        //    BewerkteMw.vestigingid = vestid;
        //    db.SaveChanges();

        //    return View();
        //}
    }
}