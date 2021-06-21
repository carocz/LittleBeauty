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
    public class Sub_categoriasController : Controller
    {
        private contextLittleBeauty db = new contextLittleBeauty();

        // GET: Sub_categorias
        public ActionResult Index()
        {
            var sub_categorias = db.Sub_categorias.Include(s => s.Categorias);
            return View(sub_categorias.ToList());
        }

        // GET: Sub_categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_categorias sub_categorias = db.Sub_categorias.Find(id);
            if (sub_categorias == null)
            {
                return HttpNotFound();
            }
            return View(sub_categorias);
        }

        // GET: Sub_categorias/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre_categoria");
            return View();
        }

        // POST: Sub_categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_sub_categoria,nombre_sub_categoria,id_categoria")] Sub_categorias sub_categorias)
        {
            if (ModelState.IsValid)
            {
                db.Sub_categorias.Add(sub_categorias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre_categoria", sub_categorias.id_categoria);
            return View(sub_categorias);
        }

        // GET: Sub_categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_categorias sub_categorias = db.Sub_categorias.Find(id);
            if (sub_categorias == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre_categoria", sub_categorias.id_categoria);
            return View(sub_categorias);
        }

        // POST: Sub_categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_sub_categoria,nombre_sub_categoria,id_categoria")] Sub_categorias sub_categorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_categorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre_categoria", sub_categorias.id_categoria);
            return View(sub_categorias);
        }

        // GET: Sub_categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_categorias sub_categorias = db.Sub_categorias.Find(id);
            if (sub_categorias == null)
            {
                return HttpNotFound();
            }
            return View(sub_categorias);
        }

        // POST: Sub_categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_categorias sub_categorias = db.Sub_categorias.Find(id);
            db.Sub_categorias.Remove(sub_categorias);
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
