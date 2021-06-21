using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Little_Beauty_2._0.Models
{
    public class ItemPedido
    {
        public int IdOrd
        {
            get;
            set;
        }
        public Productos product
        {
            get;
            set;
        }
        public int Cantidad
        {
            get;
            set;
        }
    }
}