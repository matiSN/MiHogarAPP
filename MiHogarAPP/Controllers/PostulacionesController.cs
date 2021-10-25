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
    public class PostulacionesController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: Postulaciones
        public ActionResult Index()
        {
            var postulaciones = db.Postulaciones.Include(p => p.Tutor);
            return View(postulaciones.ToList());
        }

        // GET: Postulaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postulaciones postulaciones = db.Postulaciones.Find(id);
            if (postulaciones == null)
            {
                return HttpNotFound();
            }
            return View(postulaciones);
        }

        // GET: Postulaciones/Create
        public ActionResult Create()
        {
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut");
            return View();
        }

        // POST: Postulaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Postulaciones postulaciones)
        {
            if (ModelState.IsValid)
            {
                db.Postulaciones.Add(postulaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", postulaciones.idTutor);
            return View(postulaciones);
        }

        // GET: Postulaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postulaciones postulaciones = db.Postulaciones.Find(id);
            if (postulaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", postulaciones.idTutor);
            return View(postulaciones);
        }

        // POST: Postulaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Postulaciones postulaciones)
        {
            if (ModelState.IsValid)
            {
                Postulaciones postulacionesDB = db.Postulaciones.Find(postulaciones.id);
                postulacionesDB.idTutor = postulaciones.idTutor;
                postulacionesDB.descripcion = postulaciones.descripcion;
                postulacionesDB.fecha = postulaciones.fecha;
                postulacionesDB.correo = postulaciones.correo;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", postulaciones.idTutor);
            return View(postulaciones);
        }

        // GET: Postulaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postulaciones postulaciones = db.Postulaciones.Find(id);
            if (postulaciones == null)
            {
                return HttpNotFound();
            }
            return View(postulaciones);
        }

        // POST: Postulaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Postulaciones postulaciones = db.Postulaciones.Find(id);
            db.Postulaciones.Remove(postulaciones);
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
