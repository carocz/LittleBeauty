using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Little_Beauty_2._0.Models
{
    public class ProdCarro
    {
        private contextLittleBeauty db = new contextLittleBeauty();
        private List<Productos> products;
        public ProdCarro()
        {
            products = db.Productos.ToList();
        }
        public List<Productos> findAll()
        {
            return this.products;
        }

        public Productos find(int id)
        {
            Productos pp = this.products.Single(p => p.id_producto.Equals(id));
            return pp;
        }
    }
}