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
    public class TrabajadorController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: Trabajador
        public ActionResult Index()
        {
            List<Trabajador> listado = db.Trabajador
          .Where(z => z.eliminado == false)
          .ToList();
            return View(listado);
        }

        // GET: Trabajador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabajador trabajador = db.Trabajador.Find(id);
            if (trabajador == null)
            {
                return HttpNotFound();
            }
            return View(trabajador);
        }

        // GET: Trabajador/Create
        public ActionResult Create()
        {
            ViewBag.idCargo = new SelectList(db.Cargo, "id", "descripcion");
            ViewBag.idComuna = new SelectList(db.Comuna, "id", "descripcion");
            return View();
        }

        // POST: Trabajador/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Trabajador trabajador)
        {
            Trabajador trabajadorBD = db.Trabajador.Where(x => x.rut == trabajador.rut).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (trabajadorBD==null)
                {
                    if (trabajador.telefono>0)
                    {
                        db.Trabajador.Add(trabajador);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Ingrese un numero correcto";
                    }
                }
                else
                {
                    ViewBag.Message = "El rut del trabajador ya existe";
                }

            }

            ViewBag.idCargo = new SelectList(db.Cargo, "id", "descripcion", trabajador.idCargo);
            ViewBag.idComuna = new SelectList(db.Comuna, "id", "descripcion", trabajador.idComuna);
            return View(trabajador);
        }

        // GET: Trabajador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabajador trabajador = db.Trabajador.Find(id);
            if (trabajador == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCargo = new SelectList(db.Cargo, "id", "descripcion", trabajador.idCargo);
            ViewBag.idComuna = new SelectList(db.Comuna, "id", "descripcion", trabajador.idComuna);
            return View(trabajador);
        }

        // POST: Trabajador/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Trabajador trabajador)
        {
            if (ModelState.IsValid)
            {
                Trabajador trabajadorDB = db.Trabajador.Find(trabajador.id);
                trabajadorDB.idCargo = trabajador.idCargo;
                trabajadorDB.idComuna = trabajador.idComuna;
                trabajadorDB.nombre = trabajador.nombre;
                trabajadorDB.rut = trabajador.rut;
                trabajadorDB.telefono = trabajador.telefono;
                trabajadorDB.clave = trabajador.clave;
                trabajadorDB.correo = trabajador.correo;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCargo = new SelectList(db.Cargo, "id", "descripcion", trabajador.idCargo);
            ViewBag.idComuna = new SelectList(db.Comuna, "id", "descripcion", trabajador.idComuna);
            return View(trabajador);
        }

        // GET: Trabajador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabajador trabajador = db.Trabajador.Find(id);
            if (trabajador == null)
            {
                return HttpNotFound();
            }
            return View(trabajador);
        }

        // POST: Trabajador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trabajador trabajador = db.Trabajador.Find(id);
            trabajador.fechaEliminado = DateTime.Now;
            trabajador.eliminado = true;
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
