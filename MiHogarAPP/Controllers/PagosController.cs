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
    public class PagosController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: Pagos
        public ActionResult Index()
        {
            var pagos = db.Pagos.Include(p => p.FormaPago).Include(p => p.Tutor);
            return View(pagos.ToList());
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // GET: Pagos/Create
        public ActionResult Create()
        {
            ViewBag.idFormaPago = new SelectList(db.FormaPago, "id", "descripcion");
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut");
            return View();
        }

        // POST: Pagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                if (pagos.monto>0)
                {
                    db.Pagos.Add(pagos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Ingrese un Monto valido";
                }

            }

            ViewBag.idFormaPago = new SelectList(db.FormaPago, "id", "descripcion", pagos.idFormaPago);
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", pagos.idTutor);
            return View(pagos);
        }

        // GET: Pagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idFormaPago = new SelectList(db.FormaPago, "id", "descripcion", pagos.idFormaPago);
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", pagos.idTutor);
            return View(pagos);
        }

        // POST: Pagos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                Pagos pagoDB = db.Pagos.Find(pagos.id);
                pagoDB.idTutor = pagos.idTutor;
                pagoDB.monto = pagos.monto;
                pagoDB.fecha = pagos.fecha;
                pagoDB.idFormaPago = pagos.idFormaPago;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idFormaPago = new SelectList(db.FormaPago, "id", "descripcion", pagos.idFormaPago);
            ViewBag.idTutor = new SelectList(db.Tutor, "id", "rut", pagos.idTutor);
            return View(pagos);
        }

        // GET: Pagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pagos pagos = db.Pagos.Find(id);
            db.Pagos.Remove(pagos);
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
