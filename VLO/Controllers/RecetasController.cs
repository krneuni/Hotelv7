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
    public class RecetasController : Controller
    {
        private Context db = new Context();

        // GET: Recetas
        public ActionResult Index()
        {
            var receta = db.Receta.Include(r => r.Menu).Include(r => r.Productos);
            return View(receta.ToList());
        }

        [HttpPost]
        public ActionResult Index(string txtbuscar)
        {
            var listar = db.Receta;
            var query = (from p in listar where p.Menu.Nombre.Contains(txtbuscar) select p);
            return View(query.ToList());
        }

        // GET: Recetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Receta.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            return View(receta);
        }

        // GET: Recetas/Create
        public ActionResult Create()
        {
            ViewBag.IdMenu = new SelectList(db.Menus, "IdMenu", "Nombre");
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: Recetas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdReceta,CantidadUtilizada,IdProducto,IdMenu")] Receta receta)
        {
            if (ModelState.IsValid)
            {
                db.Receta.Add(receta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMenu = new SelectList(db.Menus, "IdMenu", "Nombre", receta.IdMenu);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", receta.IdProducto);
            return View(receta);
        }

        // GET: Recetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Receta.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMenu = new SelectList(db.Menus, "IdMenu", "Nombre", receta.IdMenu);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", receta.IdProducto);
            return View(receta);
        }

        // POST: Recetas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdReceta,CantidadUtilizada,IdProducto,IdMenu")] Receta receta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMenu = new SelectList(db.Menus, "IdMenu", "Nombre", receta.IdMenu);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", receta.IdProducto);
            return View(receta);
        }

        // GET: Recetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receta receta = db.Receta.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            return View(receta);
        }

        // POST: Recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receta receta = db.Receta.Find(id);
            db.Receta.Remove(receta);
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
