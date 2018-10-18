using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerPower.Models
{
    public class ProductModel
    {
        private List<artikel> artikelen;
        private DB_A3D6D6_FlowerPowerLuukEntities db = new DB_A3D6D6_FlowerPowerLuukEntities();
        public ProductModel()
        {
            artikelen = new List<artikel>();
            foreach(artikel artikel in db.artikels.ToList())
            {
                artikelen.Add(artikel);
            }
        }

        public List<artikel> findAll()
        {
            return artikelen;
        }

        public artikel find(string id)
        {
            return artikelen.Find(p => p.artikelid.Equals(id));
        }

    }
}