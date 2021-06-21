using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Little_Beauty_2._0.Models;

namespace Little_Beauty_2._0.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private contextLittleBeauty db = new contextLittleBeauty();
        // GET: Usuario
        public ActionResult Index(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                string correo = email;
                string rol = "";

                using (db)
                {
                    var query = from st in db.Empleados
                                where st.email == correo
                                select st;
                    var lista = query.ToList();
                    if (lista.Count > 0)
                    {
                        var empleado = query.FirstOrDefault<Empleados>();
                        string[] nombres = empleado.nombre_empleado.ToString().Split(' ');
                        Session["name"] = nombres[0];
                        Session["usr"] = empleado.nombre_empleado;
                        rol = empleado.rol.ToString().TrimEnd();
                    }
                    else
                    {
                        var query1 = from st in db.Clientes
                                     where st.email == correo
                                     select st;
                        var lista1 = query1.ToList();
                        if (lista1.Count > 0)
                        {
                            var cliente = query1.FirstOrDefault<Clientes>();
                            string[] nombres = cliente.nombre_cliente.ToString().Split(' ');
                            Session["name"] = nombres[0];
                            Session["usr"] = cliente.nombre_cliente;
                            rol = "Cliente";
                        }
                    }
                }
                if (rol == "Compras")
                {
                    return RedirectToAction("Index", "Compras");
                }
                if (rol == "Administrador")
                {
                    return RedirectToAction("Index", "Administrador");
                }
                if (rol == "Almacen")
                {
                    return RedirectToAction("Index", "Almacen");
                }
                if (rol == "Cliente")
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}