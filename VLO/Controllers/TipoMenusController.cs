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
    public class TipoMenusController : Controller
    {
        private Context db = new Context();

        // GET: TipoMenus
        public ActionResult Index()
        {
            return View(db.TipoMenus.ToList());
        }

        // GET: TipoMenus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMenu tipoMenu = db.TipoMenus.Find(id);
            if (tipoMenu == null)
            {
                return HttpNotFound();
            }
            return View(tipoMenu);
        }

        // GET: TipoMenus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoMenu,Nombre")] TipoMenu tipoMenu)
        {
            if (ModelState.IsValid)
            {
                db.TipoMenus.Add(tipoMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoMenu);
        }

        // GET: TipoMenus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMenu tipoMenu = db.TipoMenus.Find(id);
            if (tipoMenu == null)
            {
                return HttpNotFound();
            }
            return View(tipoMenu);
        }

        // POST: TipoMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoMenu,Nombre")] TipoMenu tipoMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoMenu);
        }

        // GET: TipoMenus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMenu tipoMenu = db.TipoMenus.Find(id);
            if (tipoMenu == null)
            {
                return HttpNotFound();
            }
            return View(tipoMenu);
        }

        // POST: TipoMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoMenu tipoMenu = db.TipoMenus.Find(id);
            db.TipoMenus.Remove(tipoMenu);
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
