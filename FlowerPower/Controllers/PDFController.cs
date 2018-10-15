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
        private DB_A3D6D6_FlowerPowerLuukEntities2 db = new DB_A3D6D6_FlowerPowerLuukEntities2();
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
            //luuk sloopt alles
            PDFMaker PDFMaker = new PDFMaker();
            byte[] abytes = PDFMaker.PreparePDF(bestelling);

            return File(abytes, "application/pdf");
        }
    }
}