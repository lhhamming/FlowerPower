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
    public class InactiefArtikelController : Controller
    {
        private DB_A3D6D6_FlowerPowerLuukEntities db = new DB_A3D6D6_FlowerPowerLuukEntities();

        // GET: InactiefArtikel
        public ActionResult Index()
        {
            return View(db.artikels.ToList());
        }

        // GET: InactiefArtikel/Details/5
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
    }
}