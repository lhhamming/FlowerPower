using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerPower.Models;

namespace FlowerPower.Controllers
{
    public class PDFController : Controller
    {
        public ActionResult PDF(bestelling bestelling)
        {
            PDFMaker PDFMaker = new PDFMaker();
            byte[] abytes = PDFMaker.PreparePDF(GetBestellingen());

            return File(abytes, "application/pdf");
        }

        public List<bestelling> GetBestellingen()
        {
            List<bestelling> bestellingen = new List<bestelling>();
            bestelling bestelling = new bestelling();
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

            return bestellingen;
        }
    }
}