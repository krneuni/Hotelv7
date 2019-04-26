using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class PrestamosController : Controller
    {
        private Context db = new Context();

        // GET: Prestamos
        public ActionResult Index()
        {
            var prestamos = db.Prestamos.Include(p => p.Usuarios).Include(p => p.Productos);
            return View(prestamos.ToList());
        }

        // GET: Prestamos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }

        // GET: Prestamos/Create
        public ActionResult Create()
        {
            var TodosUsers = db.Usuarios.ToList();
            var Empleados = (from a in TodosUsers where a.IdRol == 1 select a).ToList();
            var EmpleadosOnly = TodosUsers.Except(Empleados);
            ViewBag.IdUser = new SelectList(EmpleadosOnly, "IdUser", "Nombre");

            var TodosProd = db.Productos.ToList();
            var Cat = db.Categoria.ToList();
            var Productos = (from p in TodosProd where p.IdCategoria==1 select p).ToList();
            var HotelOnly = TodosProd.Except(Productos);


            ViewBag.IdProducto = new SelectList(HotelOnly, "IdProducto", "Nombre");
            return View();
        }

        public ActionResult Solicitar(int? id)
        {
            ViewBag.mesa = id;
            SolicitudViewModel svm = new SolicitudViewModel();
            //svm.Prestamos = queryOrd;
            svm.Productos = db.Productos.ToList();
            svm.Categorias = db.Categoria.ToList();
            return View(svm);
        }


        [HttpPost]
        public async Task<ActionResult> AddPrest(AddPrestViewModel apvm)
        {
            //Usuarios e = db.Usuarios.Where(x => x.IdUser == 1).FirstOrDefault();
            var e = Convert.ToInt32(Session["Id"]);
            for (var i = 0; i < apvm.id.Count; i++)
            {
                Prestamos p = new Prestamos();
                p.Cantidad = apvm.cantidad[i];
                p.IdUser = e;
                p.Estado = 1;
                p.Fecha = Convert.ToString(DateTime.Now);
                p.CantidadDevuelta = 0;
                p.IdProducto = apvm.id[i];
                db.Prestamos.Add(p);
                await db.SaveChangesAsync();

                
            }  
                

            return Redirect("Espera");
        }

        //Vista de las solicitudes para el admin
        public ActionResult Solicitudes()
        {
            var soli = db.Prestamos.Where(x => x.Estado == 1).ToList();
            SolicitudViewModel cvm = new SolicitudViewModel();
            cvm.Prestamos = soli;
            cvm.Productos = db.Productos.ToList();
            return View(cvm);
        }

        [HttpGet]
        public ActionResult TerminarSolicitud(int idprestamos)
        {
            Prestamos d = db.Prestamos.Find(idprestamos);
            d.Estado = 2;
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/Prestamos/Solicitudes");
        }

        public ActionResult Espera()
        {
            var soli = db.Prestamos.Where(x => x.Estado == 2).ToList();
            SolicitudViewModel cvm = new SolicitudViewModel();
            cvm.Prestamos = soli;
            cvm.Productos = db.Productos.ToList();
            return View(cvm);

        }

        [HttpGet]
        public ActionResult Retirar(int idprestamos)
        {
            Prestamos d = db.Prestamos.Find(idprestamos);
            d.Estado = 3;
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();

            //Encontrar el producto
            var prod = db.Productos.Find(d.IdProducto);
            var Disminuye = d.Cantidad;
            double cantidad = prod.Cantidad;
            prod.Cantidad = cantidad - Disminuye;
            db.Entry(prod).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/Prestamos/Espera");
        }
        //Vista de devolucion Empleado
        public ActionResult Devolucion()
        {
            var soli = db.Prestamos.Where(x => x.Estado == 3).ToList();
            SolicitudViewModel cvm = new SolicitudViewModel();
            cvm.Prestamos = soli;
            cvm.Productos = db.Productos.ToList();
            return View(cvm);

        }


        



        //Vista Entregas Admin 
        //[HttpGet]
        public ActionResult Devolver(int idprestamos, int cant)
        {
            Prestamos d = db.Prestamos.Find(idprestamos);
            d.Estado = 4;
            d.CantidadDevuelta = cant;
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();

            //Encontrar el producto
            var prod = db.Productos.Find(d.IdProducto);
            var Disminuye = cant;
            double cantidad = prod.Cantidad;
            prod.Cantidad = cantidad + Disminuye;
            db.Entry(prod).State = EntityState.Modified;
            db.SaveChanges();



            return Redirect("/Prestamos/Devolucion");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrestamo,IdProducto,IdUser, Cantidad, Estado")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                db.Prestamos.Add(prestamos);
                db.SaveChanges();

                //Busqueda los Id de los productos que este en ambas tablas para luego aumentar
                var Existencias = (from p in db.Productos
                                   where p.IdProducto == prestamos.IdProducto
                                   select p).FirstOrDefault();
                //Aumenta el stock
                var Disminuye = prestamos.Cantidad;
                double cantidad = Existencias.Cantidad;
                Existencias.Cantidad = cantidad - Disminuye;
                db.Entry(Existencias).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", prestamos.IdUser);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", prestamos.IdProducto);
            return View(prestamos);
        }

        // GET: Prestamos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", prestamos.IdUser);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", prestamos.IdProducto);
            return View(prestamos);
        }

        // POST: Prestamos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrestamo,IdProducto,IdUser, Cantidad, Estado" +
            "")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", prestamos.IdUser);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", prestamos.IdProducto);
            return View(prestamos);
        }

        // GET: Prestamos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamos prestamos = db.Prestamos.Find(id);
            db.Prestamos.Remove(prestamos);
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
