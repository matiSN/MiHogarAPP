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
    public class CertificadosController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: Certificados
        public ActionResult Index()
        {
            var certificados = db.Certificados.Include(c => c.Trabajador);
            return View(certificados.ToList());
        }

        // GET: Certificados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificados certificados = db.Certificados.Find(id);
            if (certificados == null)
            {
                return HttpNotFound();
            }
            return View(certificados);
        }

        // GET: Certificados/Create
        public ActionResult Create()
        {
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut");
            return View();
        }

        // POST: Certificados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Certificados certificados, HttpPostedFileBase archivo2)
        {
            string ruta = Server.MapPath("/Content/certificado");
            if (ModelState.IsValid)
            {
                archivo2.SaveAs($"{ruta}/{archivo2.FileName}");
                certificados.archivo = $"/Content/certificado/{archivo2.FileName}";
                db.Certificados.Add(certificados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", certificados.idTrabajador);
            return View(certificados);
        }

        // GET: Certificados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificados certificados = db.Certificados.Find(id);
            if (certificados == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", certificados.idTrabajador);
            return View(certificados);
        }

        // POST: Certificados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Certificados certificados , HttpPostedFileBase archivo2)
        {
            string ruta = Server.MapPath("/Content/certificado");
            if (ModelState.IsValid)
            {
                Certificados certificadosBD = db.Certificados.Find(certificados.id);
                archivo2.SaveAs($"{ruta}/{archivo2.FileName}");
                certificadosBD.nombre = certificados.nombre;
                certificadosBD.descripcion = certificados.descripcion;
                certificadosBD.idTrabajador = certificados.idTrabajador;
                certificadosBD.archivo = $"/Content/certificado/{archivo2.FileName}";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", certificados.idTrabajador);
            return View(certificados);
        }

        // GET: Certificados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificados certificados = db.Certificados.Find(id);
            if (certificados == null)
            {
                return HttpNotFound();
            }
            return View(certificados);
        }

        // POST: Certificados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Certificados certificados = db.Certificados.Find(id);
            db.Certificados.Remove(certificados);
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
