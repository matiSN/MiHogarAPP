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
    public class TipoIngresoController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: TipoIngreso
        public ActionResult Index()
        {
            List<TipoIngreso> listado = db.TipoIngreso
         .Where(z => z.eliminado == false)
         .ToList();
            return View(listado);
        }

        // GET: TipoIngreso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoIngreso tipoIngreso = db.TipoIngreso.Find(id);
            if (tipoIngreso == null)
            {
                return HttpNotFound();
            }
            return View(tipoIngreso);
        }

        // GET: TipoIngreso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoIngreso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( TipoIngreso tipoIngreso)
        {
            TipoIngreso tipoIngresoDB = db.TipoIngreso.
                Where(x => x.descripcion == tipoIngreso.descripcion).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (tipoIngresoDB==null)
                {
                    db.TipoIngreso.Add(tipoIngreso);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.Message = "El Tipo de Ingreso ya Existe";
                }

            }

            return View(tipoIngreso);
        }

        // GET: TipoIngreso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoIngreso tipoIngreso = db.TipoIngreso.Find(id);
            if (tipoIngreso == null)
            {
                return HttpNotFound();
            }
            return View(tipoIngreso);
        }

        // POST: TipoIngreso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoIngreso tipoIngreso)
        {
            if (ModelState.IsValid)
            {

                TipoIngreso tipoingresoDB = db.TipoIngreso.Find(tipoIngreso.id);
                tipoingresoDB.descripcion = tipoIngreso.descripcion;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoIngreso);
        }

        // GET: TipoIngreso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoIngreso tipoIngreso = db.TipoIngreso.Find(id);
            if (tipoIngreso == null)
            {
                return HttpNotFound();
            }
            return View(tipoIngreso);
        }

        // POST: TipoIngreso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoIngreso tipoIngreso = db.TipoIngreso.Find(id);
            tipoIngreso.eliminado = true;
            tipoIngreso.fechaEliminado = DateTime.Now;
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
