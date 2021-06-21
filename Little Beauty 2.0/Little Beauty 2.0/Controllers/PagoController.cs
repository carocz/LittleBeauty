using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Little_Beauty_2._0.Models;

namespace Little_Beauty_2._0.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private contextLittleBeauty db = new contextLittleBeauty();
        private string NumConfirPago;
        // GET: Pago
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult CrearOrden()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Session["CrearOrden"] = "pend";
                return RedirectToAction("Login", "Account");
            }
            string correo = User.Identity.Name;

            var orden = new Orden();
            var db = new contextLittleBeauty();
            string fechaCreacion = DateTime.Today.ToShortDateString();
            string fechaProbEntrega = DateTime.Today.AddDays(5).ToShortDateString();
            var cliente = (from c in db.Clientes
                           where c.email == correo
                           select c).ToList().FirstOrDefault();
            Session["dirCliente"] = "México, Calle: " + cliente.direccion + " CP:" + cliente.codigo_postal;
            Session["fechaOrden"] = fechaCreacion;
            Session["fEntrega"] = fechaProbEntrega;
            if (cliente.num_tarjeta.StartsWith("4"))
                Session["tTarj"] = "1";
            if (cliente.num_tarjeta.StartsWith("5"))
                Session["tTarj"] = "2";
            if (cliente.num_tarjeta.StartsWith("6"))
                Session["tTarj"] = "3";
            Session["nTarj"] = cliente.num_tarjeta;
            return View();
        }
        public ActionResult Pagar(String tipoPago)
        {
            String correo = User.Identity.Name;
            DateTime fecchaCreacion = DateTime.Today;
            DateTime fechaprobEntega = fecchaCreacion.AddDays(5);
            var cliente = (from c in db.Clientes
                           where c.email == correo
                           select c).ToList().FirstOrDefault();

            int idClient = cliente.id_cliente;

            if (tipoPago.Equals("T"))
            {
                if (!validaPago(cliente))
                {
                    return RedirectToAction("pagoNoAceptado");
                }
                else
                {
                    return RedirectToAction("pagoAceptado", routeValues: new { idC = idClient });
                }

            }
            return View();
        }

        private bool validaPago(Clientes cliente)
        {
            bool retorna = true;

            int randomvalue;
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] val = new byte[6];
                crypto.GetBytes(val);
                randomvalue = BitConverter.ToInt32(val, 1);
            }
            NumConfirPago = Math.Abs(randomvalue * 1000).ToString();
            Session["nConfirma"] = NumConfirPago;
            return retorna;
        }

        public ActionResult pagoAceptado(int idC)
        {
            Orden orden = new Orden();
            int id = 0;
            if (!(db.Orden.Max(o => (int?)o.id_orden) == null))
            {
                id = db.Orden.Max(o => o.id_orden);
            }
            else
            {
                id = 0;
            }
            id++;
            orden.id_orden = id;
            orden.fecha_creacion = DateTime.Today;
            orden.num_confirmacion = Convert.ToInt32(Session["nConfirma"]);
            var carro = Session["cart"] as List<Item>;
            var total =  carro.Sum(item => item.Product.precio_venta * item.Cantidad);
            orden.total = Convert.ToInt32(total);
            orden.id_cliente = idC;
            db.Orden.Add(orden);
            db.SaveChanges();
            Orden_producto ordenProd;
            List<Orden_producto> listaProdOrd = new List<Orden_producto>();
            foreach(Item linea in carro)
            {
                ordenProd = new Orden_producto();
                ordenProd.id_orden = orden.id_orden;
                ordenProd.id_producto = linea.Product.id_producto;
                ordenProd.cantidad = linea.Cantidad;
                db.Orden_producto.Add(ordenProd);
            }
            db.SaveChanges();
            Session["cart"] = null;
            Session["nConfirma"] = null;
            Session["itemTotal"] = 0;
            return View();

        }

    }
}