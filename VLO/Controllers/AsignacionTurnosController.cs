using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class AsignacionTurnosController : Controller
    {
        private Context db = new Context();

        // GET: AsignacionTurnos
        public ActionResult Index()
        {
            var asignacionTurno = db.AsignacionTurno.Include(a => a.Usuarios).Include(a => a.Turnos);
            return View(asignacionTurno.ToList());
        }

        public ActionResult Turno()
        {
            //DateTime fecha = System.DateTime.Now;
            //DateTime fecha1 = System.DateTime.Now.AddDays(4);
            //var query = from x in db.AsignacionTurno
            //            where Convert.ToDateTime(x.Fecha) >= fecha && Convert.ToDateTime(x.Fecha) <= fecha1
            //            select x;
            return View(db.AsignacionTurno.ToList());
        }
        // GET: AsignacionTurnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionTurno asignacionTurno = db.AsignacionTurno.Find(id);
            if (asignacionTurno == null)
            {
                return HttpNotFound();
            }
            return View(asignacionTurno);
        }

        // GET: AsignacionTurnos/Create
        public ActionResult Create()
        {

            var TodosUsers = db.Usuarios.ToList();
            var Empleados = (from a in TodosUsers where a.IdRol == 1 select a).ToList();
            var EmpleadosOnly = TodosUsers.Except(Empleados);
            ViewBag.IdUser = new SelectList(EmpleadosOnly, "IdUser", "Nombre");
            ViewBag.IdTurno = new SelectList(db.Turnos, "IdTurno", "Nombre");
            return View();
        }

        // POST: AsignacionTurnos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAsignacion,IdTurno,Fecha,IdUser")] AsignacionTurno asignacionTurno)
        {
            if (ModelState.IsValid)
            {
                db.AsignacionTurno.Add(asignacionTurno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", asignacionTurno.IdUser);
            ViewBag.IdTurno = new SelectList(db.Turnos, "IdTurno", "Nombre", asignacionTurno.IdTurno);
            return View(asignacionTurno);
        }

        // GET: AsignacionTurnos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionTurno asignacionTurno = db.AsignacionTurno.Find(id);
            if (asignacionTurno == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", asignacionTurno.IdUser);
            ViewBag.IdTurno = new SelectList(db.Turnos, "IdTurno", "Nombre", asignacionTurno.IdTurno);
            return View(asignacionTurno);
        }

        // POST: AsignacionTurnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAsignacion,IdTurno,Fecha,IdUser")] AsignacionTurno asignacionTurno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asignacionTurno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", asignacionTurno.IdUser);
            ViewBag.IdTurno = new SelectList(db.Turnos, "IdTurno", "Nombre", asignacionTurno.IdTurno);
            return View(asignacionTurno);
        }

        // GET: AsignacionTurnos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionTurno asignacionTurno = db.AsignacionTurno.Find(id);
            if (asignacionTurno == null)
            {
                return HttpNotFound();
            }
            return View(asignacionTurno);
        }

        // POST: AsignacionTurnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AsignacionTurno asignacionTurno = db.AsignacionTurno.Find(id);
            db.AsignacionTurno.Remove(asignacionTurno);
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
