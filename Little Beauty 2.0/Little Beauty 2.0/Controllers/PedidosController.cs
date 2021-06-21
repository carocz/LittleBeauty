using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Little_Beauty_2._0.Models;
namespace Little_Beauty_2._0.Controllers
{
    [Authorize]
    public class PedidosController : Controller
    {
        contextLittleBeauty db = new contextLittleBeauty();
        // GET: Pedidos
        public ActionResult Index()
        {
            string correo = User.Identity.Name;
            Clientes cl = (from c in db.Clientes
                          where c.email == correo
                          select c
                          ).ToList().FirstOrDefault();
            int id = cl.id_cliente;
            var query = from o in db.Orden
                         where o.id_cliente == id
                         orderby o.fecha_creacion ascending
                         select o;
            List<Orden> ordenes = query.ToList();
            List<PedidoCliente> pedidos = new List<PedidoCliente>();
            PedidoCliente pedido;
            List<Orden_producto> orP;
            List<ItemPedido> itemP = new List<ItemPedido>();
            ItemPedido ip;
            foreach(Orden o in ordenes)
            {
                pedido = new PedidoCliente();
                pedido.orden = o;
                pedido.fecha = o.fecha_creacion.ToShortDateString();
                if (o.fecha_envio.HasValue)
                {
                    pedido.fecha = o.fecha_envio.GetValueOrDefault().ToShortDateString();
                }
                else
                {
                    pedido.envio = "Proximamente";
                }
                if(o.fecha_entrega.HasValue)
                {
                    pedido.status = o.fecha_entrega.GetValueOrDefault().ToShortDateString();
                }
                else
                {
                    pedido.status = "Sin entregar";
                }
                pedido.total = o.total.ToString();
                pedidos.Add(pedido);
                orP = (from oP in db.Orden_producto
                          where oP.id_orden == o.id_orden
                          select oP).ToList();
                pedido.orden_Productos = orP;
                foreach(Orden_producto op in orP)
                {
                    ip = new ItemPedido();
                    ip.IdOrd = op.id_orden;
                    ip.product = db.Productos.First(P => P.id_producto == op.id_producto);
                    ip.Cantidad = op.cantidad;
                    itemP.Add(ip);
                }


            }
            Session["misPedidos"] = pedidos;
            Session["Pedido"] = itemP;
            return View();

        }
      

    }
    
}
