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
    public class ImagenesTrabajadorController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: ImagenesTrabajador
        public ActionResult Index()
        {
            var imagenesTrabajador = db.ImagenesTrabajador.Include(i => i.Trabajador);
            return View(imagenesTrabajador.ToList());
        }

        // GET: ImagenesTrabajador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagenesTrabajador imagenesTrabajador = db.ImagenesTrabajador.Find(id);
            if (imagenesTrabajador == null)
            {
                return HttpNotFound();
            }
            return View(imagenesTrabajador);
        }

        // GET: ImagenesTrabajador/Create
        public ActionResult Create()
        {
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut");
            return View();
        }

        // POST: ImagenesTrabajador/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImagenesTrabajador imagenesTrabajador, HttpPostedFileBase imagen)
        {
            string ruta = Server.MapPath("/content/img");
            if (ModelState.IsValid)
            {
                imagen.SaveAs($"{ruta}/{imagen.FileName}");
                imagenesTrabajador.url = $"/Content/img/{imagen.FileName}";
                db.ImagenesTrabajador.Add(imagenesTrabajador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", imagenesTrabajador.idTrabajador);
            return View(imagenesTrabajador);
        }

        // GET: ImagenesTrabajador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagenesTrabajador imagenesTrabajador = db.ImagenesTrabajador.Find(id);
            if (imagenesTrabajador == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", imagenesTrabajador.idTrabajador);
            return View(imagenesTrabajador);
        }

        // POST: ImagenesTrabajador/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImagenesTrabajador imagenesTrabajador, HttpPostedFileBase imagen)
        {
            string ruta = Server.MapPath("/content/img");
            if (ModelState.IsValid)
            {
                ImagenesTrabajador imagenesTrabajadorBD = db.ImagenesTrabajador.Find(imagenesTrabajador.id);
                imagen.SaveAs($"{ruta}/{imagen.FileName}");
                imagenesTrabajadorBD.idTrabajador = imagenesTrabajador.idTrabajador;
                imagenesTrabajadorBD.url = $"/Content/img/{imagen.FileName}";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", imagenesTrabajador.idTrabajador);
            return View(imagenesTrabajador);
        }

        // GET: ImagenesTrabajador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagenesTrabajador imagenesTrabajador = db.ImagenesTrabajador.Find(id);
            if (imagenesTrabajador == null)
            {
                return HttpNotFound();
            }
            return View(imagenesTrabajador);
        }

        // POST: ImagenesTrabajador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImagenesTrabajador imagenesTrabajador = db.ImagenesTrabajador.Find(id);
            db.ImagenesTrabajador.Remove(imagenesTrabajador);
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
