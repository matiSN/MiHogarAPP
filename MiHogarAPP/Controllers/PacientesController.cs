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
    public class PacientesController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: Pacientes
        public ActionResult Index()
        {
            List <Pacientes> listado = db.Pacientes.
                Where( z => z.eliminado==false)
                .ToList();
            return View(listado);
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut");
            return View();
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                Pacientes pacientesDB = db.Pacientes.
                    Where(x => x.rut == pacientes.rut).FirstOrDefault();
                if (pacientesDB==null)
                {
                    if (pacientes.edad>0)
                    {
                        db.Pacientes.Add(pacientes);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "La edad ingresada no es valida";
                    }
                   

                }
                else
                {
                    //ViewBag.Message = "Ya existe el Paciente";
                    ViewBag.Message="El usuario ya existe";
                }

            }

            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", pacientes.idTutor);
            return View(pacientes);
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", pacientes.idTutor);
            return View(pacientes);
        }

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                Pacientes pacientesDB = db.Pacientes.Find(pacientes.id);
                pacientesDB.rut = pacientes.rut;
                pacientesDB.nombre = pacientes.nombre;
                pacientesDB.edad = pacientes.edad;
                pacientesDB.enfermedades = pacientes.enfermedades;
                pacientesDB.cobertura = pacientes.cobertura;
                pacientesDB.medicamentos = pacientes.medicamentos;
                pacientesDB.idTutor = pacientes.idTutor;
                pacientesDB.difunto = pacientes.difunto;
                pacientesDB.dependencia = pacientes.dependencia;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", pacientes.idTutor);
            return View(pacientes);
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pacientes pacientes = db.Pacientes.Find(id);
            pacientes.eliminado = true;
            pacientes.fechaEliminado = DateTime.Now;
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
