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
    public class ComunaController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: Comuna
        public ActionResult Index()
        {
            List<Comuna> listado = db.Comuna
           .Where(z => z.eliminado == false)
           .ToList();
            return View(listado);
        }

        // GET: Comuna/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comuna comuna = db.Comuna.Find(id);
            if (comuna == null)
            {
                return HttpNotFound();
            }
            return View(comuna);
        }

        // GET: Comuna/Create
        public ActionResult Create()
        {
            ViewBag.idRegion = new SelectList(db.Region, "id", "descripcion");
            return View();
        }

        // POST: Comuna/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Comuna comuna)
        {
            Comuna comunaDB = db.Comuna.Where(x => x.descripcion == comuna.descripcion).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (comunaDB==null)
                {
                    db.Comuna.Add(comuna);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "La Comuna ya Existe";
                }
 
            }

            ViewBag.idRegion = new SelectList(db.Region, "id", "descripcion", comuna.idRegion);
            return View(comuna);
        }

        // GET: Comuna/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comuna comuna = db.Comuna.Find(id);
            if (comuna == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRegion = new SelectList(db.Region, "id", "descripcion", comuna.idRegion);
            return View(comuna);
        }

        // POST: Comuna/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Comuna comuna)
        {
            if (ModelState.IsValid)
            {
                
                Comuna comunaDB2 = db.Comuna.Where(x => x.descripcion == comuna.descripcion).FirstOrDefault();
                if (comunaDB2==null)
                {
                    Comuna comunaBD = db.Comuna.Find(comuna.id);
                    comunaBD.descripcion = comuna.descripcion;
                    comunaBD.idRegion = comuna.idRegion;
                    db.SaveChanges();

                }
                else
                {
                    ViewBag.Message = "La Comuna ya Existe";
                }

                return RedirectToAction("Index");
            }
            ViewBag.idRegion = new SelectList(db.Region, "id", "descripcion", comuna.idRegion);
            return View(comuna);
        }

        // GET: Comuna/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comuna comuna = db.Comuna.Find(id);
            if (comuna == null)
            {
                return HttpNotFound();
            }
            return View(comuna);
        }

        // POST: Comuna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comuna comuna = db.Comuna.Find(id);
            comuna.eliminado = true;
            comuna.fechaEliminado = DateTime.Now;
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
