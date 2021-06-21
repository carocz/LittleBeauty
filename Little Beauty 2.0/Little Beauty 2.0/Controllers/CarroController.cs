using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Little_Beauty_2._0.Models;

namespace Little_Beauty_2._0.Controllers
{
    public class CarroController : Controller
    {
        // GET: Carro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Agregar(int id)
        {
            ProdCarro carro = new ProdCarro();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                Productos P = carro.find(id);
                string nam = P.nombre_producto;
                cart.Add(new Item { Product = carro.find(id), Cantidad = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Cantidad++;
                }
                else
                {
                    Productos P = carro.find(id);
                    string nam = P.nombre_producto;
                    cart.Add(new Item { Product = carro.find(id), Cantidad = 1 });
                }
                Session["cart"] = cart;
                Session["itemTotal"] = contador();
            }
            
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.id_producto.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        private int contador()
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int aux= 0;
            for (int i = 0; i < cart.Count; i++)
            {
                aux += cart[i].Cantidad;
                
            }
            return aux;
        }
        public ActionResult Quitar(int id)
        {
            ProdCarro carro = new ProdCarro();
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            if (index != -1)
            {
                if(cart[index].Cantidad == 1)
                {
                    Productos P = carro.find(id);
                    string nam = P.nombre_producto;
                    cart.RemoveAt(index);
                }
                else
                {
                    cart[index].Cantidad--;
                }
            }
            else
            {
                Productos P = carro.find(id);
                string nam = P.nombre_producto;
                cart.RemoveAt(index);
            }
            Session["itemTotal"] = contador();
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
    }
}