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
        [Authorize(Roles = "Applicatiebeheerder, Manager, Medewerker, Klant")]
        public ActionResult Index()
        {
            
            // Get user id
            var user = User.Identity.GetUserId();
            // True/False variables depending on role
            bool UserAdmin = User.IsInRole("Applicatiebeheerder");
            bool UserManager = User.IsInRole("Manager");
            bool UserMedewerker = User.IsInRole("Medewerker");
            // Show all bestellings if Applicatiebeheerder or Manager
            if (UserAdmin || UserManager)
            {

                var result = db.bestellings.ToList();
                return View(result);
            }
            // Show only unfinished orders of employee vestiging if employee is active
            else if (User.IsInRole("Medewerker"))
            {
                var CurrentMedewerker = db.medewerkers.Where(m => m.AspNetUserID == user).FirstOrDefault();
                if(CurrentMedewerker.actief == true)
                {
                    var bList = db.bestellings.Where(b => b.medewerkerid == null && (b.statusid == 1 || b.statusid == 2) && b.vestigingid == CurrentMedewerker.vestigingsid);

                    var result = bList;

                    return View(result);
                }
                else
                {
                    return View();
                }

                
            }
            // Klant can only see own orders
            else
            {

                var CurrentKlant = db.klants.Where(k => k.AspNetUserID == user).FirstOrDefault();

                var bList = db.bestellings.Where(m => m.klantid == CurrentKlant.klantid);

                var result = bList;
                return View(result);
            }

        }
        [Authorize(Roles = "Medewerker")]
        // GET: bestelling/Take/5
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
        // Set order to finished
        // POST: Take/5
        [HttpPost, ActionName("Take")]
        [ValidateAntiForgeryToken]
        public ActionResult TakeConfirmed(int? id)
        {
            //Get user id
            var user = User.Identity.GetUserId();
            //Get current medewerker user
            var CurrentMedewerker = db.medewerkers.Where(m => m.AspNetUserID == user).FirstOrDefault();
            //Find current bestelling
            bestelling bestelling = db.bestellings.Find(id);
            //Set employee that marked the bestelling as finished
            bestelling.medewerkerid = CurrentMedewerker.medewerkerid;
            //Set finished status
            bestelling.statusid = 3;
            db.SaveChanges();


            return RedirectToAction("Index");
        }
        // GET: Afhalen/5
        [Authorize(Roles = "Medewerker")]
        public ActionResult Afhalen(int? id)
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
        // POST: Afhalen/5
        [HttpPost, ActionName("Afhalen")]
        [ValidateAntiForgeryToken]
        public ActionResult AfhalenConfirmed(int? id)
        {
            var user = User.Identity.GetUserId();
            var CurrentMedewerker = db.medewerkers.Where(m => m.AspNetUserID == user).FirstOrDefault();

            bestelling bestelling = db.bestellings.Find(id);

            bestelling.medewerkerid = CurrentMedewerker.medewerkerid;
            bestelling.statusid = 2;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Klant")]
        public ActionResult Annuleren(int? id)
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

        [HttpPost, ActionName("Annuleren")]
        [ValidateAntiForgeryToken]
        public ActionResult AnnulerenConfirmed(int? id)
        {
            
            bestelling bestelling = db.bestellings.Find(id);
            //Change status to canceled if not canceled or finished
            if (bestelling.statusid != 4 && bestelling.statusid != 3)
            {
                bestelling.statusid = 4;
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Index");
            }    


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
            // Create pdf file from bestelling data
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
        // TODO: change status instead of del.
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
