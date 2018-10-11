using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerPower.Models;
using System.Net;

namespace FlowerPower.Controllers
{
    public class PDFController : Controller
    {
        private DB_A3D6D6_FlowerPowerLuukEntities1 db = new DB_A3D6D6_FlowerPowerLuukEntities1();
        // GET: PDF/PDF/5
        public ActionResult PDF(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bestelling bestelling  = db.bestellings.Find(id);
            if (bestelling == null)
            {
                return HttpNotFound();
            }
            PDFMaker PDFMaker = new PDFMaker();
            byte[] abytes = PDFMaker.PreparePDF(GetBestellingen(bestelling));

            return File(abytes, "application/pdf");
        }

        public List<bestelling> GetBestellingen(bestelling bestelling)
        {

            /*
            for (int i = 1; i <= 8; i++)
            {
                student = new Student();
                student.Id = i;
                student.Voornaam = "Henk" + i;
                student.Achternaam = "Pietjan" + i;
                student.Geboortedatum = DateTime.Now;
                students.Add(student);
            }*/

            //return bestellingen;
            return null;
        }
    }
}