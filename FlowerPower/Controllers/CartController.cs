using FlowerPower.Models;
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

        public ActionResult Buy(int id)
        {
            ProductModel productModel = new ProductModel();
            if (Session["cart"] == null)
            {
                List<item> cart = new List<item>();
                cart.Add(new item {
                    Product = db.artikels.Find(id), Quantity = 1
                });
                Session["cart"] = cart;
            }
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
                    cart.Add(new item { Product = db.artikels.Find(id), Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(string id)
        {
            List<item> cart = (List<item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index + 1 );
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

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