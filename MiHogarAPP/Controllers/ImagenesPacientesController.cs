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
    public class ImagenesPacientesController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: ImagenesPacientes
        public ActionResult Index()
        {
            var imagenesPacientes = db.ImagenesPacientes.Include(i => i.Pacientes);
            return View(imagenesPacientes.ToList());
        }

        // GET: ImagenesPacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagenesPacientes imagenesPacientes = db.ImagenesPacientes.Find(id);
            if (imagenesPacientes == null)
            {
                return HttpNotFound();
            }
            return View(imagenesPacientes);
        }

        // GET: ImagenesPacientes/Create
        public ActionResult Create()
        {
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut");
            return View();
        }

        // POST: ImagenesPacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImagenesPacientes imagenesPacientes, HttpPostedFileBase imagen)
        {
            string ruta = Server.MapPath("/content/img");
            if (ModelState.IsValid)
            {
                imagen.SaveAs($"{ruta}/{imagen.FileName}");
                imagenesPacientes.url = $"/Content/img/{imagen.FileName}";
                db.ImagenesPacientes.Add(imagenesPacientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", imagenesPacientes.idPaciente);
            return View(imagenesPacientes);
        }

        // GET: ImagenesPacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagenesPacientes imagenesPacientes = db.ImagenesPacientes.Find(id);
            if (imagenesPacientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", imagenesPacientes.idPaciente);
            return View(imagenesPacientes);
        }

        // POST: ImagenesPacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImagenesPacientes imagenesPacientes, HttpPostedFileBase imagen)
        {
            string ruta = Server.MapPath("/content/img");
            if (ModelState.IsValid)
            {
                ImagenesPacientes imagenesPacientesBD = db.ImagenesPacientes.Find(imagenesPacientes.id);
                imagen.SaveAs($"{ruta}/{imagen.FileName}");
                imagenesPacientesBD.idPaciente = imagenesPacientes.idPaciente;
                imagenesPacientesBD.url = $"/Content/img/{imagen.FileName}";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "id", "rut", imagenesPacientes.idPaciente);
            return View(imagenesPacientes);
        }

        // GET: ImagenesPacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagenesPacientes imagenesPacientes = db.ImagenesPacientes.Find(id);
            if (imagenesPacientes == null)
            {
                return HttpNotFound();
            }
            return View(imagenesPacientes);
        }

        // POST: ImagenesPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImagenesPacientes imagenesPacientes = db.ImagenesPacientes.Find(id);
            db.ImagenesPacientes.Remove(imagenesPacientes);
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
