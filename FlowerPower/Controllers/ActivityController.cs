using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerPower.Controllers
{
    public class ActivityController : Controller
    {


        // GET: Activity
        public ActionResult Index()
        {


            return View();
        }

        // GET: MederwerkerList
        public ActionResult MedewerkerList(int? id)
        {
            return View();
        }

        // POST: Deactivate/id/5
        public ActionResult Deactivate(int? id)
        {
            return View();
        }
        // POST: Activate/id/5
        public ActionResult Activate(int? id)
        {
            return View();
        }
    }
}