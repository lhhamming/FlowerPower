using FlowerPower.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerPower.Controllers
{
    [Authorize(Roles = "Manager, Applicatiebeheerder")]
    public class RolesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (true)
                {
                    //Als de ingelogde gebruiker geen administrator is dan sturen we deze gebruiker terug naar de
                    //Index pagina. Dit doen we zodat het lijkt als of hier niks te zien is
                    var Roles = context.Roles.ToList();
                    return View(Roles);
                    //return RedirectToAction("Index", "Home");
                }
                else
                {

                    var Roles = context.Roles.ToList();
                    return View(Roles);
                }
            }
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateNewRole(IdentityRole collection)
        {
            try
            {
                //Add role in database en save
                context.Roles.Add(collection);
                context.SaveChanges();

                //Responds
                ViewBag.Message = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();


            }
        }

        public ActionResult Remove()
        {
            var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = rolelist;
            return View();
        }

        public ActionResult Link()
        {

            //We geven hier een ViewBag mee waar items in zitten die we bij de pagina Link openen en op hun plek zetten
            var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = rolelist;
            //We doen precies het zelfde hier. 
            var userlist = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
            new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userlist;

            var Users = context.Users.ToList();
            return View(Users);
        }

        public ActionResult AddUserTo(string UserName, String RoleName)
        {
            try
            {
                //We moeten de lists opnieuw vullen. Dit doen voor als we de pagina terug linken naar de zelfde pagina dat de lijsten dan niet leeg zijn
                var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = rolelist;
                var userlist = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
                    new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
                ViewBag.Users = userlist;

                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var roles = userManager.GetRoles(user.Id).ToList();

                //Als hij de role al heeft dan word het niet toegevoegd.
                foreach (var role in roles)
                {
                    if (role == RoleName)
                    {
                        ViewBag.Succes = $"{user.UserName} heeft de role {role} al.";
                        return View("Link");
                    }
                }

                //De user kan maar in èèn role zitten, de oude role word verwijderd en de nieuwe word toegevoegd.
                if (roles.Count == 1)
                {
                    string rolename = roles.FirstOrDefault();
                    userManager.RemoveFromRole(user.Id, rolename);
                    userManager.AddToRole(user.Id, RoleName);

                    ViewBag.Succes = $"{rolename} is verwijderd en {RoleName} is toegevoegd aan {user.UserName}";
                    return View("Link");
                }

                //Toevoeging van de role
                userManager.AddToRole(user.Id, RoleName);

                ViewBag.Succes = $"{RoleName} is toegevoegd aan {user.UserName}";
                return View("Link");
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong!";
                return View("Error");
            }
        }


        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Applicatiebeheerder")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}