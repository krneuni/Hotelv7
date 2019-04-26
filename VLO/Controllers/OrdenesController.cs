using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class OrdenesController : Controller
    {
        private Context db = new Context();

        // GET: Ordenes
        public ActionResult Index()
        {
            OrdenesViewModel All = new OrdenesViewModel();
            All.DetallePedido = db.DetallePedido.ToList();
            All.Pedido = db.Pedido.ToList();
            All.Mesa = db.Mesa.ToList();
            return View(All);
        }

        public ActionResult Ordenes()
        {
            var orden = db.Pedido./*Where(x => x.Estado == 1).*/ToList();
            var detalle = db.DetallePedido.Where(x => x.Estado == 1).ToList();
            CocinaViewModel cvm = new CocinaViewModel();
            cvm.pedidos = orden;
            cvm.detalle = detalle;
            cvm.menus = db.Menus.ToList();
            

            
            return View(cvm);
        }

        


        [HttpGet]
        public ActionResult TerminarOrdenCocina(int iddetalle)
        {
            
            DetallePedido dp = db.DetallePedido.Find(iddetalle);
            dp.Estado = 2;
            db.Entry(dp).State = EntityState.Modified;
            db.SaveChanges();

            var pe = db.Pedido.Find(dp.IdPedido);
            var detped = (from u in db.DetallePedido where u.IdPedido == pe.IdPedido select u).ToList();
            //Recorrer
            foreach (var io in detped)
            {
                
               
                Pedido de = db.Pedido.Find(io.IdPedido);
                de.Estado = 2;
                db.Entry(de).State = EntityState.Modified;
                db.SaveChanges();

            }


            //var menu = db.Menus.Find(dp.IdMenu);
            ////Buscar los menus en la receta
            //var recmenu = (from u in db.Receta where u.IdMenu == menu.IdMenu select u).ToList();
            ////Recorrer
            //foreach (var io in recmenu)
            //{
            //    //Encontrar los productos que se utilizan
            //    Productos de = db.Productos.Find(io.IdProducto);

            //    //Resta de la cantidad que se pide menos la cantidad utilizada

            //    var Descuento = io.CantidadUtilizada * dp.cantidad;
            //    de.Cantidad = de.Cantidad - Descuento;
            //    db.Entry(de).State = EntityState.Modified;
            //    db.SaveChanges();
            //}

            return Redirect("/Ordenes/Ordenes");
        }

        public ActionResult OrdenesTerminadas()
        {
            var orden = db.Pedido/*.Where(x => x.Estado == 2)*/.ToList();
            var detalle = db.DetallePedido.Where(x => x.Estado == 2).ToList();
            CocinaViewModel cvm = new CocinaViewModel();
            cvm.pedidos = orden;
            cvm.detalle = detalle;
            cvm.menus = db.Menus.ToList();
            return View(cvm);
        }

        public ActionResult OrdenesMeseros(int idpedido)
        {
            Session["pedidoid"] = idpedido;
            //Pedido p = db.Pedido.Find(idpedido);
            //p.Estado = 3;
            //db.Entry(p).State = EntityState.Modified;
            //db.SaveChanges();

            return Redirect("/Ordenes/Pagos");
        }

        



        //VistaMenu
        public ActionResult Menu(int? id, int? idPedido)
        {
            Session["pedido"] = idPedido;
            Session["mesa"] = id;

            ViewBag.mesa = id;
           
                //var dp = db.DetallePedido.Max(m => m.sesion);
                //Session["session"] = dp + 1;
                var pedido = Convert.ToInt32(Session["pedido"]);
            var mesa= Convert.ToInt32(Session["mesa"]);
            if (pedido>0)
                {
                    var cli = (from p in db.Pedido where p.IdPedido == pedido && p.IdMesa==mesa select p).FirstOrDefault();
                    ViewBag.cliente = cli.Cliente;
                    ViewBag.cantidad = cli.Cantidad;
                }
            
            
            var queryOrd = db.DetallePedido.Where(d => d.IdPedido == idPedido).ToList();
            OrdenesViewModel ovm = new OrdenesViewModel();
            ovm.DetallePedido = queryOrd;
            ovm.Menus = db.Menus.ToList();
            ovm.TiposMenu = db.TipoMenus.ToList();
            return View(ovm);
        }


        //Agregar orden y guardar datos en las diferentes tablas
        [HttpPost]
        public async Task<ActionResult> AddOrden(AddOrdenViewModel aovm)
        {
            //Usuarios e = db.Usuarios.Where(x => x.IdUser == 1).FirstOrDefault();
            int user = Convert.ToInt32(Session["Id"]);

            var ps = Convert.ToInt32(Session["pedido"]);
            if ( ps== 0)
            {
                Pedido p = new Pedido();
                p.Cantidad = aovm.numpersonas;
                p.Cliente = aovm.cliente;
                p.IdUser = user;
                p.Estado = 1;
                p.IdMesa = aovm.mesa;
            db.Pedido.Add(p);
            await db.SaveChangesAsync();
                Session["pedido"] = p.IdPedido;
                Session["mesa"] = p.Mesa;
            }
            

            var lastPedido = db.Pedido.OrderByDescending(x=>x.IdPedido).First();
            for(var i= 0;i < aovm.id.Count;i++)
            {
                DetallePedido dp = new DetallePedido();
                dp.IdMenu = aovm.id[i];
                dp.cantidad = aovm.cantidad[i];
                dp.IdPedido = Convert.ToInt32(Session["pedido"]);
                dp.sesion = Convert.ToInt32(Session["session"]);
                dp.Estado = 1;
                dp.Termino = aovm.termino;
                db.DetallePedido.Add(dp);
                await db.SaveChangesAsync();

                //Encontrar el menu
                var menu = db.Menus.Find(dp.IdMenu);
                //Buscar los menus en la receta
                var recmenu = (from u in db.Receta where u.IdMenu == menu.IdMenu select u).ToList();
                //Recorrer
                foreach (var io in recmenu)
                {
                    //Encontrar los productos que se utilizan
                    Productos de = db.Productos.Find(io.IdProducto);
                    
                    //Resta de la cantidad que se pide menos la cantidad utilizada

                    var Descuento = io.CantidadUtilizada * dp.cantidad;
                    de.Cantidad =de.Cantidad - Descuento;
                    db.Entry(de).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            //Encontrar las mesas
            Mesa d = db.Mesa.Find(aovm.mesa);
            //Cambia el estado de la mesa
            d.Estado = false;
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("Index");
        }

        //public async Task<ActionResult> DeleteDetalle(string key, string key2)
        //{
        //    try
        //    {
        //        var header = new Dictionary<string, string>()
        //        {
        //            {"IdDetalle",key }
        //        };
        //        DetallePedido dp = db.DetallePedido.Find(key);
        //        db.DetallePedido.Remove(dp);
        //        await db.SaveChangesAsync();
        //    }
        //    catch { }
        //    return RedirectToAction("Menu", new { IdMesa = key2 });
        //}

        [HttpGet]
        public ActionResult Pagos()
        {
            var idpedido = Convert.ToInt32(Session["pedidoid"]);
            var orden = db.Pedido.Where(x => x.Estado == 2).ToList();
            var queryOrd = db.DetallePedido.Where(d => d.IdPedido == idpedido).ToList();
            CocinaViewModel cvm = new CocinaViewModel();
            cvm.pedidos = orden;
            cvm.detalle = queryOrd;
            cvm.menus = db.Menus.ToList();
            return View(cvm);
        }

        [HttpPost]
        public ActionResult RealizarPago(double txttotal,int idDetalle, double Descuento, string Descripcion, double propina, AddOrdenViewModel cvm)
        {
            
            Session["pedido"] = 0;
            Session["mesa"] = 0;
            Session["pedidoid"]= 0;

            Factura p = new Factura();
            //p.NumFactura =1;
            p.IdDetalle = idDetalle;
            p.Precio = txttotal;
            p.Descuento = Descuento;
            p.Descripcion = Descripcion;
            p.Propina = propina;
            p.FechaFactura = Convert.ToString(DateTime.Now.Date);
            db.Factura.Add(p);
            db.SaveChanges();


            var detalle = db.DetallePedido.Find(p.IdDetalle);
            var det = (from x in db.DetallePedido where x.IdDetalle == detalle.IdDetalle select x).ToList();
            foreach (var i in det)
            {
                DetallePedido de = db.DetallePedido.Find(i.IdDetalle);
                de.Estado = 3;
                db.Entry(de).State = EntityState.Modified;
                db.SaveChanges();


            }

            //Buscar cada Pedido
            var pedido = (from u in db.Pedido where u.IdPedido == detalle.IdPedido select u).ToList();
            //Recorrer
            foreach (var io in pedido)
            {
                //DetallePedido de = db.DetallePedido.Find(io.IdPedido);
                //de.Estado = 3;
                //db.Entry(de).State = EntityState.Modified;
                //db.SaveChanges();

                Pedido pe = db.Pedido.Find(io.IdPedido);
                pe.Estado = 3;
                db.Entry(pe).State = EntityState.Modified;
                db.SaveChanges();
            }

            //var mesadetalle = db.DetallePedido.Find(p.IdDetalle);
            //var detallepedido = (from m in db.Pedido where m.IdPedido == mesadetalle.IdPedido select m).FirstOrDefault();
            //var pedidomesa = (from x in db.Mesa where x.IdMesa == detallepedido.IdMesa select x);

            //Mesa d = db.Mesa.Find(pedidomesa);
            //d.Estado = true;
            //db.Entry(d).State = EntityState.Modified;
            //db.SaveChanges();
            return Redirect("Index");
        }
        

    }
}