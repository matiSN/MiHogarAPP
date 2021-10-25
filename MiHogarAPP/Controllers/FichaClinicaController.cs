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
    public class FichaClinicaController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: FichaClinica
        public ActionResult Index()
        {
            var fichaClinica = db.FichaClinica.Include(f => f.Pacientes).Include(f => f.TipoIngreso).Include(f => f.Trabajador);
            return View(fichaClinica.ToList());
        }

        // GET: FichaClinica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FichaClinica fichaClinica = db.FichaClinica.Find(id);
            if (fichaClinica == null)
            {
                return HttpNotFound();
            }
            return View(fichaClinica);
        }

        // GET: FichaClinica/Create
        public ActionResult Create()
        {
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut");
            ViewBag.idTipoIngreso = new SelectList(db.TipoIngreso, "id", "descripcion");
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut");
            return View();
        }

        // POST: FichaClinica/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( FichaClinica fichaClinica)
        {
            if (ModelState.IsValid)
            {
                fichaClinica.fecha = DateTime.Now;
                db.FichaClinica.Add(fichaClinica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", fichaClinica.idPaciente);
            ViewBag.idTipoIngreso = new SelectList(db.TipoIngreso, "id", "descripcion", fichaClinica.idTipoIngreso);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", fichaClinica.idTrabajador);
            return View(fichaClinica);
        }

        // GET: FichaClinica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FichaClinica fichaClinica = db.FichaClinica.Find(id);
            if (fichaClinica == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", fichaClinica.idPaciente);
            ViewBag.idTipoIngreso = new SelectList(db.TipoIngreso, "id", "descripcion", fichaClinica.idTipoIngreso);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", fichaClinica.idTrabajador);
            return View(fichaClinica);
        }

        // POST: FichaClinica/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( FichaClinica fichaClinica)
        {
            if (ModelState.IsValid)
            {
                FichaClinica fichaClinicaDB = db.FichaClinica.Find(fichaClinica.id);
                fichaClinicaDB.idPaciente = fichaClinica.idPaciente;
                fichaClinicaDB.idTipoIngreso = fichaClinica.idTipoIngreso;
                fichaClinicaDB.fecha= fichaClinica.fecha;
                fichaClinicaDB.aseoBucal= fichaClinica.aseoBucal;
                fichaClinicaDB.aseoGenital= fichaClinica.aseoGenital;
                fichaClinicaDB.aseoCavidades= fichaClinica.aseoCavidades;
                fichaClinicaDB.hidratacion= fichaClinica.hidratacion;
                fichaClinicaDB.bano= fichaClinica.bano;
                fichaClinicaDB.desposiciones= fichaClinica.desposiciones;
                fichaClinicaDB.tempAxilar= fichaClinica.tempAxilar;
                fichaClinicaDB.frecuenciaResp= fichaClinica.frecuenciaResp;
                fichaClinicaDB.cuidadoUnaPelo= fichaClinica.cuidadoUnaPelo;
                fichaClinicaDB.daoc = fichaClinica.daoc;
                fichaClinicaDB.pulso= fichaClinica.pulso;
                fichaClinicaDB.presionArterial= fichaClinica.presionArterial;
                fichaClinicaDB.saturacion= fichaClinica.saturacion;
                fichaClinicaDB.diuresis= fichaClinica.diuresis;
                fichaClinicaDB.vomitos= fichaClinica.vomitos;
                fichaClinicaDB.otros= fichaClinica.otros;
                fichaClinicaDB.observaciones= fichaClinica.observaciones;
                fichaClinicaDB.idTrabajador= fichaClinica.idTrabajador;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", fichaClinica.idPaciente);
            ViewBag.idTipoIngreso = new SelectList(db.TipoIngreso, "id", "descripcion", fichaClinica.idTipoIngreso);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", fichaClinica.idTrabajador);
            return View(fichaClinica);
        }

        // GET: FichaClinica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FichaClinica fichaClinica = db.FichaClinica.Find(id);
            if (fichaClinica == null)
            {
                return HttpNotFound();
            }
            return View(fichaClinica);
        }

        // POST: FichaClinica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FichaClinica fichaClinica = db.FichaClinica.Find(id);
            db.FichaClinica.Remove(fichaClinica);
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
