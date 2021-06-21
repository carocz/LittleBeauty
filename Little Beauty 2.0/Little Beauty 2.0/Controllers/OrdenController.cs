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
    public class OrdenController : Controller
    {
        private contextLittleBeauty db = new contextLittleBeauty();

        // GET: Orden
        public ActionResult Index()
        {
            var orden = db.Orden.Where(o => o.fecha_envio == null).OrderBy(o => o.fecha_creacion).Include(o => o.Clientes).Include(o => o.Paquetes); 
            return View(orden.ToList());
        }
        public ActionResult Index1()
        {
            var orden = db.Orden.Where(o => o.fecha_entrega == null && o.fecha_envio != null).OrderBy(o => o.fecha_creacion).Include(o => o.Clientes).Include(o => o.Paquetes);
            return View(orden.ToList());
        }

        // GET: Orden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = db.Orden.Find(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // GET: Orden/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.Clientes, "id_cliente", "nombre_cliente");
            ViewBag.id_paquete = new SelectList(db.Paquetes, "id_paquete", "direccion_entrega");
            return View();
        }

        // POST: Orden/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_orden,fecha_creacion,num_confirmacion,total,id_cliente,id_paquete,direccion,num_guia,fecha_envio,fecha_entrega")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                db.Orden.Add(orden);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.Clientes, "id_cliente", "nombre_cliente", orden.id_cliente);
            ViewBag.id_paquete = new SelectList(db.Paquetes, "id_paquete", "direccion_entrega", orden.id_paquete);
            return View(orden);
        }

        // GET: Orden/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = db.Orden.Find(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.Clientes, "id_cliente", "nombre_cliente", orden.id_cliente);
            ViewBag.id_paquete = new SelectList(db.Paquetes, "id_paquete", "direccion_entrega", orden.id_paquete);
            return View(orden);
        }
        public ActionResult Edit1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = db.Orden.Find(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.Clientes, "id_cliente", "nombre_cliente", orden.id_cliente);
            ViewBag.id_paquete = new SelectList(db.Paquetes, "id_paquete", "direccion_entrega", orden.id_paquete);
            return View(orden);
        }
        // POST: Orden/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_orden,id_paquete,num_guia,fecha_envio")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                Orden o = db.Orden.Find(orden.id_orden);
                o.id_orden = orden.id_orden;
                o.id_paquete = orden.id_paquete;
                o.num_guia = orden.num_guia;
                o.fecha_envio = orden.fecha_envio;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.Clientes, "id_cliente", "nombre_cliente", orden.id_cliente);
            ViewBag.id_paquete = new SelectList(db.Paquetes, "id_paquete", "direccion_entrega", orden.id_paquete);
            return View(orden);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "id_orden,fecha_entrega")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                Orden o = db.Orden.Find(orden.id_orden);
                o.id_orden = orden.id_orden;
                o.fecha_entrega = orden.fecha_entrega;
                db.SaveChanges();
                return RedirectToAction("Index1");
            }
            ViewBag.id_cliente = new SelectList(db.Clientes, "id_cliente", "nombre_cliente", orden.id_cliente);
            ViewBag.id_paquete = new SelectList(db.Paquetes, "id_paquete", "direccion_entrega", orden.id_paquete);
            return View(orden);
        }

        // GET: Orden/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = db.Orden.Find(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // POST: Orden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orden orden = db.Orden.Find(id);
            db.Orden.Remove(orden);
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
