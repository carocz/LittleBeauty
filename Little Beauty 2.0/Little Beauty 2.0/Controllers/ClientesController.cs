using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Little_Beauty_2._0.Models;
using System.Text;
using Microsoft.AspNet.Identity;

namespace Little_Beauty_2._0.Controllers
{
    public class ClientesController : Controller
    {
        private contextLittleBeauty db = new contextLittleBeauty();

        // GET: Clientes
        public ActionResult Index()
        {
            var clientes = db.Clientes.Include(c => c.Entidades).Include(c => c.Municipios).Include(c => c.Paises);
            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad");
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio");
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais");
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id_cliente,id_pais,id_entidad,id_municipio,nombre_cliente,direccion,apellido_cliente,codigo_postal,num_tarjeta,telefono,email,password")] Clientes clientes)
        //******************************************************************************************
        public ActionResult Create(int id_pais, int id_entidad, int id_municipio,string nombre_cliente, string direccion, string apellido_cliente, string codigo_postal, string num_tarjeta,long telefono, string email , string password,string TipoTarj, string Mes, string Anio, string CVV)
        {
            Clientes clientes = new Clientes();
            int id = 0;
            if (!(db.Clientes.Max(c => (int?)c.id_cliente) == null))
            {
                id = db.Clientes.Max(c => c.id_cliente);
            }
            else
            {
                id = 0;
            }
            id++;

            if (Tarjeta(num_tarjeta, TipoTarj, Mes, Anio, CVV))
            {
                if (validaPago(nombre_cliente, codigo_postal, direccion, id_entidad, num_tarjeta, Mes, Anio, CVV))
                {
                    clientes.id_cliente = id;
                    clientes.id_pais = id_pais;
                    clientes.id_entidad = id_entidad;
                    clientes.id_municipio = id_municipio;
                    clientes.nombre_cliente = nombre_cliente;
                    clientes.email = Session["correo"].ToString();
                    clientes.apellido_cliente = apellido_cliente;
                    clientes.codigo_postal = codigo_postal;
                    clientes.direccion= direccion;
                    clientes.num_tarjeta = num_tarjeta;
                    clientes.telefono = telefono;
                    clientes.password = password;
                    db.Clientes.Add(clientes);
                    db.SaveChanges();

                    string[] nombres = clientes.nombre_cliente.ToString().Split(' ');
                    Session["name"] = nombres[0];
                    Session["usr"] = clientes.nombre_cliente;

                    if (Session["CrearOrden"] != null)
                    {
                        if (Session["CrearOrden"].Equals("pend"))
                        {
                            return RedirectToAction("CrearOrden", "Pago");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Invalida");
                }

            }
            else
            {
                return RedirectToAction("Invalida");
            }

            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad", clientes.id_entidad);
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio", clientes.id_municipio);
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais", clientes.id_pais);
            return View(clientes);
        }

        private bool Tarjeta(string tarj, string tipo, string mes, string anio, string cvv)
        {
            bool retorna = validaTar(tarj);
            if (retorna)
            {
                if ((tarj.StartsWith("4")) && (tipo.Equals("Visa")))
                {
                    retorna = true;
                }
                else
                {
                    if ((tarj.StartsWith("5")) && (tipo.Equals("Mastercard")))
                    {
                        retorna = true;
                    }
                    else
                    {
                        if ((tarj.StartsWith("3")) && (tipo.Equals("American")))
                        {
                            retorna = true;
                        }
                        else
                        {
                            retorna = false;
                        }
                    }
                }
                DateTime hoy = new DateTime();
                if (Convert.ToInt32(anio) >= hoy.Year)
                {
                    if (Convert.ToInt32(mes) >= hoy.Month)
                    {
                        retorna = true;
                    }
                    else
                    {
                        retorna = false;
                    }
                }
                else
                {
                    retorna = false;
                }
            }
            return retorna;
        }

        private bool validaTar(string tarj)
        {
            bool retorna = true;
            StringBuilder digitsOnly = new StringBuilder();
            foreach (Char c in tarj)
            {
                if (Char.IsDigit(c)) digitsOnly.Append(c);
            };
            if (digitsOnly.Length > 18 || digitsOnly.Length < 15) return false;
            int sum = 0, digit = 0, addend = 0;
            bool timesTwo = false;
            for (int i = digitsOnly.Length - 1; i >= 0; i--)
            {
                digit = Int32.Parse(digitsOnly.ToString(i, 1));
                if (timesTwo)
                {
                    addend = digit * 2;
                    if (addend > 9)
                    {
                        addend -= 9;
                    }
                }
                else
                {
                    addend = digit;
                }
                sum += addend;
                timesTwo = !timesTwo;
            }
            retorna = ((sum % 10) == 0);
            return retorna;
        }

        public ActionResult Invalida()
        {
            return View();
        }

        public ActionResult BorraUser()
        {
            string idUser = User.Identity.GetUserId();
            return RedirectToAction("Delete", "Account", routeValues: new { id = idUser });
        }

        private bool validaPago(string nombre, string codigo_postal, string direccion, int id_entidad, string num_tarjeta, string Mes, string Anio, string CVV)
        {
            bool retorna = true;

            return retorna;
        }
        //*******************************************************************************************
        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad", clientes.id_entidad);
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio", clientes.id_municipio);
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais", clientes.id_pais);
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cliente,id_pais,id_entidad,id_municipio,nombre_cliente,direccion,apellido_cliente,codigo_postal,num_tarjeta,telefono,email,password")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad", clientes.id_entidad);
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio", clientes.id_municipio);
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais", clientes.id_pais);
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clientes clientes = db.Clientes.Find(id);
            db.Clientes.Remove(clientes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
