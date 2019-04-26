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
    public class DetallePedidosController : Controller
    {
        private Context db = new Context();

        // GET: DetallePedidos
        public ActionResult Index()
        {
            var detallePedido = db.DetallePedido.Include(d => d.Menu).Include(d => d.Pedido);
            return View(detallePedido.ToList());
        }

        // GET: DetallePedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // GET: DetallePedidos/Create
        public ActionResult Create()
        {

            //Muestre los pedidos
            var TodasPedidos = db.Pedido.ToList();
            //Muestra la union de las dos tablas
            var ide = (Session["pedidoid"]);
            int id = Convert.ToInt32(ide);
            var Clientes = (from p in db.Pedido where p.IdPedido == id select p).ToList();
            
            //Muestra los clientes que  estan en esa mesa
            //var ClientMesa = TodasPedidos.Except(Clientes);

            ViewBag.IdMenu = new SelectList(db.Menus, "IdMenu", "Nombre");
            ViewBag.IdPedido = new SelectList(Clientes, "IdPedido", "Cliente");
            return View();
        }

        // POST: DetallePedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetalle,IdMenu,IdPedido, cantidad")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                db.DetallePedido.Add(detallePedido);
                db.SaveChanges();
                //Busqueda los Id los menus que este en ambas tablas para luego descontar
                var DescontarMenus = (from p in db.Receta
                                      where p.IdMenu == detallePedido.IdMenu
                                      select p).ToList();

                foreach (var i in DescontarMenus)
                {
                    //Encontrar los productos que se utilizan
                    Productos d = db.Productos.Find(i.IdProducto);
                    //Resta de la cantidad que se pide menos la cantidad utilizada

                    var Descuento = i.CantidadUtilizada * detallePedido.cantidad;

                    d.Cantidad = d.Cantidad - Descuento;
                    db.Entry(d).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.IdMenu = new SelectList(db.Menus, "IdMenu", "Nombre", detallePedido.IdMenu);
            ViewBag.IdPedido = new SelectList(db.Pedido, "IdPedido", "Cliente", detallePedido.IdPedido);
            
            return View(detallePedido);
        }

        // GET: DetallePedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMenu = new SelectList(db.Menus, "IdMenu", "Nombre", detallePedido.IdMenu);
            ViewBag.IdPedido = new SelectList(db.Pedido, "IdPedido", "Cliente", detallePedido.IdPedido);
           
            return View(detallePedido);
        }

        // POST: DetallePedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetalle,IdMenu,IdPedido,cantidad")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallePedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMenu = new SelectList(db.Menus, "IdMenu", "Nombre", detallePedido.IdMenu);
            ViewBag.IdPedido = new SelectList(db.Pedido, "IdPedido", "Cliente", detallePedido.IdPedido);
            
            return View(detallePedido);
        }

        // GET: DetallePedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // POST: DetallePedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            db.DetallePedido.Remove(detallePedido);
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
