using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConexionMiHogarAPP.Models;

namespace MiHogarAPP.Controllers
{
    public class ProductoController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: Producto
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.Pacientes).Include(p => p.TipoProducto).Include(p => p.Trabajador).Include(p => p.Tutor);
            return View(producto.ToList());
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut");
            ViewBag.idTipoProducto = new SelectList(db.TipoProducto, "id", "descripcion");
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut");
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut");
            return View();
        }

        // POST: Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            Producto productoDB = db.Producto.Where(x => x.nombreProducto == producto.nombreProducto).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (producto.cantidad>0)
                {
                    producto.fechaIngreso = DateTime.Now;
                    db.Producto.Add(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "La Cantidad Ingresada no es Valida";
                }

            }

            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", producto.idPaciente);
            ViewBag.idTipoProducto = new SelectList(db.TipoProducto, "id", "descripcion", producto.idTipoProducto);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", producto.idTrabajador);
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", producto.idTutor);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", producto.idPaciente);
            ViewBag.idTipoProducto = new SelectList(db.TipoProducto, "id", "descripcion", producto.idTipoProducto);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", producto.idTrabajador);
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", producto.idTutor);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                Producto productoDB = db.Producto.Find(producto.id);
                productoDB.descripcion= productoDB.descripcion;
                productoDB.idPaciente = productoDB.idPaciente;
                productoDB.idTipoProducto = productoDB.idTipoProducto;
                productoDB.cantidad = productoDB.cantidad;
                productoDB.idTrabajador = productoDB.idTrabajador;
                productoDB.fechaIngreso = productoDB.fechaIngreso;
                productoDB.idTutor = productoDB.idTutor;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", producto.idPaciente);
            ViewBag.idTipoProducto = new SelectList(db.TipoProducto, "id", "descripcion", producto.idTipoProducto);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", producto.idTrabajador);
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", producto.idTutor);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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
