using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Little_Beauty_2._0.Models;

namespace Little_Beauty_2._0.Controllers
{
    public class CatalogoController : Controller
    {
        private contextLittleBeauty db = new contextLittleBeauty();
        // GET: Catalogo
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult BuscaProd(string nomBuscar)
        {
            ViewBag.SearchKey = nomBuscar;
            using (db)
            {
                var query = from st in db.Productos
                            where st.nombre_producto.Contains(nomBuscar)
                            select st;

                var listProd = query.ToList();
                ViewBag.Listado = listProd;
            }
            return View();
        }
        public ActionResult prodCategoria(int idCat)
        {
            List<Productos> mercancia = null;
            var query = from p in db.Productos
                        where p.id_apartamento == idCat
                        select p;

            if (idCat == 1)
            {
                List<Productos> Cabello = query.ToList();
                mercancia = Cabello;
                ViewBag.Catego = "Cuidado del cabello";

            }
            if (idCat == 2)
            {
                List<Productos> Maquillaje = query.ToList();
                mercancia = Maquillaje;
                ViewBag.Catego = "Maquillaje";

            }
            if (idCat == 1002)
            {
                List<Productos> piel = query.ToList();
                mercancia = piel;
                ViewBag.Catego = "Cuidado de la piel";

            }
            if (idCat == 1003)
            {
                List<Productos> Unias = query.ToList();
                mercancia = Unias;
                ViewBag.Catego = "Uñas";

            }
            ViewBag.productos = mercancia;
            return View();
        }

    }
}