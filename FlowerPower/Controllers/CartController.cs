using FlowerPower.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerPower.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private DB_A3D6D6_FlowerPowerLuukEntities db = new DB_A3D6D6_FlowerPowerLuukEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        // Add artikel with quantity to cart session
        public ActionResult Buy(int id, int quant)
        {
            ProductModel productModel = new ProductModel();
            // Make cart session if none exists and add an item to the cart
            if (Session["cart"] == null)
            {
                List<item> cart = new List<item>();
                cart.Add(new item {
                    Product = db.artikels.Find(id), Quantity = quant
                });
                Session["cart"] = cart;
            }
            // If cart session exists add item to existing cart
            else
            {
                List<item> cart = (List<item>)Session["cart"];
                int index = isExist(id.ToString());
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new item { Product = db.artikels.Find(id), Quantity = quant });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        //Remove item from cart
        public ActionResult Remove(string id)
        {
            List<item> cart = (List<item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index + 1 );
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        // GET: Order
        public ActionResult Order()
        {

            ViewBag.vestigingid = new SelectList(db.vestigings, "vestigingsid", "vestigingsnaam");

            return View();
        }
        // POST: Order
        [HttpPost, ActionName("Order")]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCreate([Bind(Include = "bestellingid,ophaaldatum,vestigingid")] bestelling bestelling)
        {
            //assign userid to variable
            var userid = User.Identity.GetUserId();
            //selectlist of vestigings in vestigingid ViewBag
            ViewBag.vestigingid = new SelectList(db.vestigings, "vestigingsid", "vestigingsnaam", bestelling.vestigingid);
            // Get current klant
            var cUser = db.klants.Where(k => k.AspNetUserID == userid).FirstOrDefault();

            // fill model fields that are not bound
            bestelling.besteldatum = DateTime.Now;
            bestelling.statusid = 1;
            bestelling.bestelregelid = 1;
            bestelling.klantid = cUser.klantid;

            //Add bestelling to the database
            if (ModelState.IsValid)
            {
                db.bestellings.Add(bestelling);
                
                //Create a bestelregel for every item in the cart
                foreach (item item in (List<item>)Session["cart"])
                {
                    var bestelregel = new bestelregel();

                    bestelregel.artikel_artikelid = item.Product.artikelid;
                    bestelregel.bestelling_bestellingid = bestelling.bestellingid;
                    bestelregel.aantal = item.Quantity;
                    db.bestelregels.Add(bestelregel);
                    
                }
                //Save database changes
                db.SaveChanges();
                //Empty cart after placing bestelling
                EmptyCart();
                return RedirectToAction("Index", "bestelling", new { area = ""});
            }
            
            return View(bestelling);
        }
        //Empties cart session
        private void EmptyCart()
        {
            Session["cart"] = null;
        }

        //Checks for existing artikels in the cart
        private int isExist(string id)
        {
            List<item> cart = (List<item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.artikelid.Equals(id))
                    return i;
            return -1;
        }

    }
}