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
    public class ApartamentosController : Controller
    {
        private contextLittleBeauty db = new contextLittleBeauty();

        // GET: Apartamentos
        public ActionResult Index()
        {
            var apartamentos = db.Apartamentos.Include(a => a.Empleados);
            return View(apartamentos.ToList());
        }

        // GET: Apartamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartamentos apartamentos = db.Apartamentos.Find(id);
            if (apartamentos == null)
            {
                return HttpNotFound();
            }
            return View(apartamentos);
        }

        // GET: Apartamentos/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado_encargado = new SelectList(db.Empleados, "id_empleado", "nombre_empleado");
            return View();
        }

        // POST: Apartamentos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_apartamento,nombre_apartamento,id_empleado_encargado,descripcion")] Apartamentos apartamentos)
        {
            if (ModelState.IsValid)
            {
                db.Apartamentos.Add(apartamentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado_encargado = new SelectList(db.Empleados, "id_empleado", "nombre_empleado", apartamentos.id_empleado_encargado);
            return View(apartamentos);
        }

        // GET: Apartamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartamentos apartamentos = db.Apartamentos.Find(id);
            if (apartamentos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado_encargado = new SelectList(db.Empleados, "id_empleado", "nombre_empleado", apartamentos.id_empleado_encargado);
            return View(apartamentos);
        }

        // POST: Apartamentos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_apartamento,nombre_apartamento,id_empleado_encargado,descripcion")] Apartamentos apartamentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartamentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado_encargado = new SelectList(db.Empleados, "id_empleado", "nombre_empleado", apartamentos.id_empleado_encargado);
            return View(apartamentos);
        }

        // GET: Apartamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartamentos apartamentos = db.Apartamentos.Find(id);
            if (apartamentos == null)
            {
                return HttpNotFound();
            }
            return View(apartamentos);
        }

        // POST: Apartamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apartamentos apartamentos = db.Apartamentos.Find(id);
            db.Apartamentos.Remove(apartamentos);
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
