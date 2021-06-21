using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Little_Beauty_2._0.Models;
namespace Little_Beauty_2._0.Models
{
    
    public class PedidoCliente
    {
        private contextLittleBeauty db = new contextLittleBeauty();
        private List<Orden_producto> detalle_orden;

        public PedidoCliente()
        {
            detalle_orden = db.Orden_producto.ToList();
        }
        public Orden orden
        {
            get;
            set;
        }
        public string fecha
        {
            get;
            set;
        }
        public string envio
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }
        public string total
        {
            get;
            set;
        }
        public List<Orden_producto> orden_Productos
        {
            get;
            set;
        }
    }
}