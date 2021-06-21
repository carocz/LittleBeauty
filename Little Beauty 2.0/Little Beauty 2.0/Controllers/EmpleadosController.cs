using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Little_Beauty_2._0.Models;

namespace Little_Beauty_2._0.Controllers
{
    [Authorize]
    public class EmpleadosController : Controller
    {
        private contextLittleBeauty db = new contextLittleBeauty();

        // GET: Empleados
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Entidades).Include(e => e.Municipios).Include(e => e.Paises);
            return View(empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad");
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio");
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais");
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_empleado,id_pais,id_entidad,id_municipio,nombre_empleado,direccion,apellido_empleado,telefono,rol,email,password")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {

                db.Empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad", empleados.id_entidad);
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio", empleados.id_municipio);
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais", empleados.id_pais);
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad", empleados.id_entidad);
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio", empleados.id_municipio);
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais", empleados.id_pais);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_empleado,id_pais,id_entidad,id_municipio,nombre_empleado,direccion,apellido_empleado,telefono,rol,email,password")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad", empleados.id_entidad);
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio", empleados.id_municipio);
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais", empleados.id_pais);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
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
