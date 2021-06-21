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
    public class ProveedoresController : Controller
    {
        private contextLittleBeauty db = new contextLittleBeauty();

        // GET: Proveedores
        public ActionResult Index()
        {
            var proveedores = db.Proveedores.Include(p => p.Entidades).Include(p => p.Municipios).Include(p => p.Paises);
            return View(proveedores.ToList());
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad");
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio");
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais");
            return View();
        }

        // POST: Proveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_proveedor,id_pais,id_entidad,id_municipio,nombre_proveedor,direccion,apellido_proveedor,rfc,codigo_postal,telefono,email,password")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Proveedores.Add(proveedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad", proveedores.id_entidad);
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio", proveedores.id_municipio);
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais", proveedores.id_pais);
            return View(proveedores);
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad", proveedores.id_entidad);
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio", proveedores.id_municipio);
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais", proveedores.id_pais);
            return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_proveedor,id_pais,id_entidad,id_municipio,nombre_proveedor,direccion,apellido_proveedor,rfc,codigo_postal,telefono,email,password")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_entidad = new SelectList(db.Entidades, "id_entidad", "nombre_entidad", proveedores.id_entidad);
            ViewBag.id_municipio = new SelectList(db.Municipios, "id_municipio", "nombre_municipio", proveedores.id_municipio);
            ViewBag.id_pais = new SelectList(db.Paises, "Id_pais", "nombre_pais", proveedores.id_pais);
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedores proveedores = db.Proveedores.Find(id);
            db.Proveedores.Remove(proveedores);
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
