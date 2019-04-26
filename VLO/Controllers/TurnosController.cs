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
    public class TurnosController : Controller
    {
        private Context db = new Context();

        // GET: Turnos
        public ActionResult Index()
        {
            return View(db.Turnos.ToList());
        }

        // GET: Turnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnos turnos = db.Turnos.Find(id);
            if (turnos == null)
            {
                return HttpNotFound();
            }
            return View(turnos);
        }

        // GET: Turnos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTurno,Nombre,HoraInicial,HoraFinal")] Turnos turnos)
        {
            if (ModelState.IsValid)
            {
                db.Turnos.Add(turnos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turnos);
        }

        // GET: Turnos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnos turnos = db.Turnos.Find(id);
            if (turnos == null)
            {
                return HttpNotFound();
            }
            return View(turnos);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTurno,Nombre,HoraInicial,HoraFinal")] Turnos turnos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turnos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turnos);
        }

        // GET: Turnos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnos turnos = db.Turnos.Find(id);
            if (turnos == null)
            {
                return HttpNotFound();
            }
            return View(turnos);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turnos turnos = db.Turnos.Find(id);
            db.Turnos.Remove(turnos);
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
