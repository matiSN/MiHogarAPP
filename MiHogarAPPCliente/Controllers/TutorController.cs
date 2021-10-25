using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConexionMiHogarAPP.Models;

namespace MiHogarAPPCliente.Controllers
{
    public class TutorController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: Tutor
        public ActionResult Index()
        {
            var tutor = db.Tutor.Include(t => t.Comuna);
            return View(tutor.ToList());
        }

        // GET: Tutor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutor tutor = db.Tutor.Find(id);
            if (tutor == null)
            {
                return HttpNotFound();
            }
            return View(tutor);
        }

        // GET: Tutor/Create
        public ActionResult Create()
        {
            ViewBag.idComuna = new SelectList(db.Comuna, "id", "descripcion");
            return View();
        }

        // POST: Tutor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,rut,nombre,telefono,correo,direccion,idComuna,clave,eliminado,fechaEliminado")] Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                db.Tutor.Add(tutor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idComuna = new SelectList(db.Comuna, "id", "descripcion", tutor.idComuna);
            return View(tutor);
        }

        // GET: Tutor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutor tutor = db.Tutor.Find(id);
            if (tutor == null)
            {
                return HttpNotFound();
            }
            ViewBag.idComuna = new SelectList(db.Comuna, "id", "descripcion", tutor.idComuna);
            return View(tutor);
        }

        // POST: Tutor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,rut,nombre,telefono,correo,direccion,idComuna,clave,eliminado,fechaEliminado")] Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idComuna = new SelectList(db.Comuna, "id", "descripcion", tutor.idComuna);
            return View(tutor);
        }

        // GET: Tutor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutor tutor = db.Tutor.Find(id);
            if (tutor == null)
            {
                return HttpNotFound();
            }
            return View(tutor);
        }

        // POST: Tutor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tutor tutor = db.Tutor.Find(id);
            db.Tutor.Remove(tutor);
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
