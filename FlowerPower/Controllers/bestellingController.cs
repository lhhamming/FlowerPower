using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlowerPower.Models;
using Microsoft.AspNet.Identity;

namespace FlowerPower.Controllers
{
    public class bestellingController : Controller
    {
        private DB_A3D6D6_FlowerPowerLuukEntities db = new DB_A3D6D6_FlowerPowerLuukEntities();

        private List<bestelling> BestellingenPerVestiging = new List<bestelling>();

        // GET: bestelling
        [Authorize(Roles = "ApplicatieBeheerder, Manager, Medewerker, Klant")]
        public ActionResult Index()
        {
            

            var user = User.Identity.GetUserId();
            bool UserAdmin = User.IsInRole("ApplicatieBeheerder");
            bool UserManager = User.IsInRole("Manager");
            bool UserMedewerker = User.IsInRole("Medewerker");

            //Get the current medewerker
            

            if (User.IsInRole("Klant"))
            {

                var CurrentKlant = db.klants.Where(k => k.AspNetUserID == user).FirstOrDefault();

                var bList = db.bestellings.Where(m => m.klantid == CurrentKlant.klantid);

                var result = bList;
                return View(result);
            }

            else if (UserAdmin || UserManager)
            {

                var result = db.bestellings.ToList();
                return View(result);
            }
            else
            {
                var CurrentMedewerker = db.medewerkers.Where(m => m.AspNetUserID == user).FirstOrDefault();

                var bList = db.bestellings.Where(b => b.medewerkerid == CurrentMedewerker.medewerkerid && b.statusid == 1);

                var result = bList;

                return View(result);
            }
            


            //else
            //{
            //    if (UserMedewerker)
            //    {
            //        //De gebruiker is een klant of een medewerker
            //        foreach (var Bestelling in db.bestellings.ToList())
            //        {
            //            if (Bestelling.vestiging.vestigingsid == CurrentMedewerker.First().vestigingsid)
            //            {
            //                BestellingenPerVestiging.Add(Bestelling);
            //            }
            //        }
            //    }
            //    else
            //    {

            //    }
                

            //}
        }

        // GET: bestelling/Take/5

        // TODO: Authorization for medewerker/ View
        public ActionResult Take(int? id)
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

        [HttpPost, ActionName("Take")]
        [ValidateAntiForgeryToken]
        public ActionResult TakeConfirmed(int? id)
        {
            // TODO: Post medewerker association to bestelling
            var user = User.Identity.GetUserId();
            var CurrentMedewerker = db.medewerkers.Where(m => m.AspNetUserID == user).FirstOrDefault();

            bestelling bestelling = db.bestellings.Find(id);

            bestelling.medewerkerid = CurrentMedewerker.medewerkerid;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        // GET: bestelling/Factuur/5
        public ActionResult Factuur(int? id)
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

            PDFMaker PDFMaker = new PDFMaker();
            byte[] abytes = PDFMaker.PreparePDF(bestelling);

            return File(abytes, "application/pdf");
        }


        // GET: bestelling/Details/5
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

        // GET: bestelling/Create
        public ActionResult Create()
        {
            ViewBag.klantid = new SelectList(db.klants, "klantid", "voorletters");
            ViewBag.medewerkerid = new SelectList(db.medewerkers, "medewerkerid", "voorletters");
            ViewBag.statusid = new SelectList(db.status, "statusid", "status1");
            ViewBag.vestigingid = new SelectList(db.vestigings, "vestigingsid", "vestigingsnaam");
            return View();
        }

        // POST: bestelling/Create
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

        // GET: bestelling/Edit/5
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

        // POST: bestelling/Edit/5
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

        public ActionResult Afgehandeld(int id)
        {
            
            bestelling bestelling = db.bestellings.Find(id);
            //Status ID 2 betekent dat hij klaar is voor het afhalen.
            bestelling.statusid = 2;
            return View();
        }

        // GET: bestelling/Delete/5
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

        // POST: bestelling/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bestelling bestelling = db.bestellings.Find(id);
            var vandaag = DateTime.Now;
            if (bestelling.besteldatum >= vandaag || bestelling.besteldatum <= vandaag)
            {
                //Id 4 is geannuleerd.
                bestelling.statusid = 4;
                db.SaveChanges();
                ViewBag.Succes = "Geannuleerd";
                return RedirectToAction("Index");
            }
            ViewBag.Error = "U kunt uw bestelling niet annuleren op de dag dat uw bestelling geweest is.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PlaceOrder()
        {

            return Index();
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
