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
    public class DetalleComprasController : Controller
    {
        private Context db = new Context();

        // GET: DetalleCompras
        public ActionResult Index()
        {
            var detalleCompra = db.DetalleCompra.Include(d => d.Productos).Include(d => d.Proveedores);
            return View(detalleCompra.ToList());
        }

        //[HttpPost]
        //public ActionResult Index(string txtbuscar, string Opciones)
        //{
        //    var pro = db.Proveedores.FirstOrDefault();
        //    var query = from tbl in db.DetalleCompra select tbl;
        //    //validacion para que la caja no traiga datos vacios
        //    string formato = txtbuscar.Trim();
        //    if (formato != "")
        //    {
        //        if (Opciones == "0")
        //        {
        //            if (Opciones == "1")
        //            {
        //                query = from proveedor in db.DetalleCompra where proveedor.IdProveedor == pro.IdProveedor select proveedor;
        //            }

        //            if (Opciones == "2")
        //            {
        //                query = (from fecha in db.DetalleCompra where fecha.FechaCompra.Contains(txtbuscar) select fecha);
        //            }
        //        }

        //    }
        //        return View(query.ToList());
        //}
            // GET: DetalleCompras/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Create
        public ActionResult Create()
        {
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre");
            ViewBag.IdProveedor = new SelectList(db.Proveedores, "IdProveedor", "Nombre");
            return View();
        }

        // POST: DetalleCompras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetalle,Cantidad,PrecioUnit,FechaCompra,PrecioTotal,Codigo,IdProveedor,IdProducto")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                
                db.DetalleCompra.Add(detalleCompra);
                db.SaveChanges();


                ////Cambiar el estado del PrecioTotal
                
                double VU = detalleCompra.PrecioUnit;
                double Cantidad = detalleCompra.Cantidad;

                detalleCompra.PrecioTotal = VU * Cantidad;
                db.Entry(detalleCompra).State = EntityState.Modified;
                db.SaveChanges();
                

                //Busqueda los Id de los productos que este en ambas tablas para luego aumentar
                var Existencias = (from p in db.Productos
                                   where p.IdProducto == detalleCompra.IdProducto
                                   select p).FirstOrDefault();
                //Aumenta el stock
                var Aumento = detalleCompra.Cantidad;
                double cantidad = Existencias.Cantidad;
                Existencias.Cantidad = cantidad + Aumento;
                db.Entry(Existencias).State = EntityState.Modified;
                db.SaveChanges();
                



                return RedirectToAction("Index");
            }

            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", detalleCompra.IdProducto);
            ViewBag.IdProveedor = new SelectList(db.Proveedores, "IdProveedor", "Nombre", detalleCompra.IdProveedor);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", detalleCompra.IdProducto);
            ViewBag.IdProveedor = new SelectList(db.Proveedores, "IdProveedor", "Nombre", detalleCompra.IdProveedor);
            return View(detalleCompra);
        }

        // POST: DetalleCompras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetalle,Cantidad,PrecioUnit,FechaCompra,PrecioTotal,Codigo,IdProveedor,IdProducto")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", detalleCompra.IdProducto);
            ViewBag.IdProveedor = new SelectList(db.Proveedores, "IdProveedor", "Nombre", detalleCompra.IdProveedor);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            return View(detalleCompra);
        }

        // POST: DetalleCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            db.DetalleCompra.Remove(detalleCompra);
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
