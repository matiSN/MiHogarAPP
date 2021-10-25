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
    public class HorarioTrabajadorController : Controller
    {
        private MihogarAPPEntities db = new MihogarAPPEntities();

        // GET: HorarioTrabajador
        public ActionResult Index()
        {
            var horarioTrabajador = db.HorarioTrabajador.Include(h => h.Trabajador);
            return View(horarioTrabajador.ToList());
        }

        // GET: HorarioTrabajador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HorarioTrabajador horarioTrabajador = db.HorarioTrabajador.Find(id);
            if (horarioTrabajador == null)
            {
                return HttpNotFound();
            }
            return View(horarioTrabajador);
        }

        // GET: HorarioTrabajador/Create
        public ActionResult Create()
        {
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut");
            return View();
        }

        // POST: HorarioTrabajador/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( HorarioTrabajador horarioTrabajador)
        {
            DateTime ahora = DateTime.Now;
            DateTime ahora2 = ahora;
            


            if (ModelState.IsValid)
            {
                //string fecha = $"{horarioTrabajador.fecha.Day}/{horarioTrabajador.fecha.Month}/{horarioTrabajador.fecha.Year} {ahora.Hour}:{ahora.Minute}:{ahora.Second}";
                //ahora2 = Convert.ToDateTime(fecha);
                //int resultado = DateTime.Compare(ahora2, ahora);
                //if (resultado>=0)
                //{
                //    db.HorarioTrabajador.Add(horarioTrabajador);
                //    db.SaveChanges();
                //    return RedirectToAction("Index");

                //}
                //else
                //{
                //    ViewBag("La fecha no es Valida");
                //}
                string fecha = $"{horarioTrabajador.fecha.Day}/{horarioTrabajador.fecha.Month}/{horarioTrabajador.fecha.Year} {ahora.Hour}:{ahora.Minute}:{ahora.Second}";
                ahora2 = Convert.ToDateTime(fecha);
                if (ahora2.Year > ahora.Year)
                {
                    db.HorarioTrabajador.Add(horarioTrabajador);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else if (ahora2.Year == ahora.Year)
                {
                    if (ahora2.Month > ahora.Month)
                    {
                        db.HorarioTrabajador.Add(horarioTrabajador);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (ahora2.Month == ahora.Month)
                    {
                        if (ahora2.Day >= ahora.Day)
                        {
                            db.HorarioTrabajador.Add(horarioTrabajador);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message="El dia no es Valido";
                        }
                    }
                    else
                    {
                        ViewBag.Message="El mes no es Valido";
                    }
                }
                else
                {
                    ViewBag.Message="El año no es Valido";
                }


            }

            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", horarioTrabajador.idTrabajador);
            return View(horarioTrabajador);
        }

        // GET: HorarioTrabajador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HorarioTrabajador horarioTrabajador = db.HorarioTrabajador.Find(id);
            if (horarioTrabajador == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", horarioTrabajador.idTrabajador);
            return View(horarioTrabajador);
        }

        // POST: HorarioTrabajador/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( HorarioTrabajador horarioTrabajador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(horarioTrabajador).State = EntityState.Modified;
                HorarioTrabajador horarioTrabajadorBD = db.HorarioTrabajador.Find(horarioTrabajador.id);
                horarioTrabajadorBD.fecha = horarioTrabajador.fecha;
                horarioTrabajadorBD.horaEntrada = horarioTrabajador.horaEntrada;
                horarioTrabajadorBD.horaSalida = horarioTrabajador.horaSalida;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "id", "rut", horarioTrabajador.idTrabajador);
            return View(horarioTrabajador);
        }

        // GET: HorarioTrabajador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HorarioTrabajador horarioTrabajador = db.HorarioTrabajador.Find(id);
            if (horarioTrabajador == null)
            {
                return HttpNotFound();
            }
            return View(horarioTrabajador);
        }

        // POST: HorarioTrabajador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HorarioTrabajador horarioTrabajador = db.HorarioTrabajador.Find(id);
            db.HorarioTrabajador.Remove(horarioTrabajador);
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
