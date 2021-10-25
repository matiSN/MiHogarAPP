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
    public class VisitasController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: Visitas
        public ActionResult Index()
        {
            var visitas = db.Visitas.Include(v => v.Pacientes).Include(v => v.Trabajador).Include(v => v.Tutor);
            return View(visitas.ToList());
        }

        // GET: Visitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            return View(visitas);
        }

        // GET: Visitas/Create
        public ActionResult Create()
        {
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut");
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut");
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut");
            return View();
        }

        // POST: Visitas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Visitas visitas)
        {
            if (ModelState.IsValid)
            {
                db.Visitas.Add(visitas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", visitas.idPaciente);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", visitas.idTrabajador);
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", visitas.idTutor);
            return View(visitas);
        }

        // GET: Visitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", visitas.idPaciente);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", visitas.idTrabajador);
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", visitas.idTutor);
            return View(visitas);
        }

        // POST: Visitas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Visitas visitas)
        {
            if (ModelState.IsValid)
            {
                Visitas visitasDB = db.Visitas.Find(visitas.id);
                visitasDB.idTutor = visitas.idTutor;
                visitasDB.idPaciente = visitas.idPaciente;
                visitasDB.fecha = visitas.fecha;
                visitasDB.hora = visitas.hora;
                visitasDB.idTrabajador = visitas.idTrabajador;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", visitas.idPaciente);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", visitas.idTrabajador);
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", visitas.idTutor);
            return View(visitas);
        }

        // GET: Visitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            return View(visitas);
        }

        // POST: Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitas visitas = db.Visitas.Find(id);
            db.Visitas.Remove(visitas);
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
