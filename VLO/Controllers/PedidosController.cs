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
    public class PedidosController : Controller
    {
        private Context db = new Context();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedido = db.Pedido.Include(p => p.Usuarios).Include(p => p.Mesa);
            return View(pedido.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            //Muestre los mesas 
            var TodasMesas = db.Mesa.ToList();
            //Muestra la union de las dos tablas
            var MesasOcupadas = (from a in db.Mesa
                                 join m in db.Pedido on a.IdMesa equals m.IdMesa
                                 select a).ToList();
            //Muestra las mesas que  estan disponibles
            var mesasDisponibles = TodasMesas.Except(MesasOcupadas);

            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre");
            ViewBag.IdMesa = new SelectList(mesasDisponibles, "IdMesa", "NumMesa");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPedido,Cantidad,Cliente,IdMesa,IdUser")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedido.Add(pedido);
                db.SaveChanges();

                //Para cambiar el estado de la disponibilidad de mesa
                var mesas = (from a in db.Mesa where a.IdMesa == pedido.IdMesa select a).FirstOrDefault();
                mesas.Estado = false;
                db.Entry(mesas).State = EntityState.Modified;
                db.SaveChanges();
                Session["pedidoid"] = pedido.IdPedido;
                return RedirectToAction("Create","DetallePedidos");
            }

            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", pedido.IdUser);
            ViewBag.IdMesa = new SelectList(db.Mesa, "IdMesa", "NumMesa", pedido.IdMesa);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", pedido.IdUser);
            ViewBag.IdMesa = new SelectList(db.Mesa, "IdMesa", "NumMesa", pedido.IdMesa);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPedido,Cantidad,Cliente,IdMesa,IdUser")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", pedido.IdUser);
            ViewBag.IdMesa = new SelectList(db.Mesa, "IdMesa", "NumMesa", pedido.IdMesa);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedido.Find(id);
            db.Pedido.Remove(pedido);
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
