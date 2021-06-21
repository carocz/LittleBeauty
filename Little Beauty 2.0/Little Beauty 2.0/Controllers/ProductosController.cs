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
    public class ProductosController : Controller
    {
        private contextLittleBeauty db = new contextLittleBeauty();

        // GET: Productos
        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.Apartamentos).Include(p => p.Categorias).Include(p => p.Proveedores).Include(p => p.Sub_categorias);
            return View(productos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.id_apartamento = new SelectList(db.Apartamentos, "id_apartamento", "nombre_apartamento");
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre_categoria");
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "id_proveedor", "nombre_proveedor");
            ViewBag.id_sub_categoria = new SelectList(db.Sub_categorias, "id_sub_categoria", "nombre_sub_categoria");
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_producto,id_proveedor,nombre_producto,id_apartamento,id_categoria,id_sub_categoria,marca,modelo,descripcion,precio_compra,precio_venta,imagen,stock")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(productos);
                db.SaveChanges();
                int id = productos.id_producto;
                //-------CAMBIAMOS EL ID DEL PRODUCTO
                var prod = db.Productos.Find(id);
                DateTime hoy = DateTime.Now;
                prod.ult_actualizacion = hoy;
                db.SaveChanges();
                //-------CAMBIAMOS EL ID DEL PRODUCTO
                return RedirectToAction("Index");
            }

            ViewBag.id_apartamento = new SelectList(db.Apartamentos, "id_apartamento", "nombre_apartamento", productos.id_apartamento);
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre_categoria", productos.id_categoria);
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "id_proveedor", "nombre_proveedor", productos.id_proveedor);
            ViewBag.id_sub_categoria = new SelectList(db.Sub_categorias, "id_sub_categoria", "nombre_sub_categoria", productos.id_sub_categoria);
            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_apartamento = new SelectList(db.Apartamentos, "id_apartamento", "nombre_apartamento", productos.id_apartamento);
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre_categoria", productos.id_categoria);
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "id_proveedor", "nombre_proveedor", productos.id_proveedor);
            ViewBag.id_sub_categoria = new SelectList(db.Sub_categorias, "id_sub_categoria", "nombre_sub_categoria", productos.id_sub_categoria);
            return View(productos);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_producto,id_proveedor,nombre_producto,id_apartamento,id_categoria,id_sub_categoria,marca,modelo,descripcion,precio_compra,precio_venta,ult_actualizacion,imagen,stock")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_apartamento = new SelectList(db.Apartamentos, "id_apartamento", "nombre_apartamento", productos.id_apartamento);
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "nombre_categoria", productos.id_categoria);
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "id_proveedor", "nombre_proveedor", productos.id_proveedor);
            ViewBag.id_sub_categoria = new SelectList(db.Sub_categorias, "id_sub_categoria", "nombre_sub_categoria", productos.id_sub_categoria);
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productos productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
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
