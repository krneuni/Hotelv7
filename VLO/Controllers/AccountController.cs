using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class AccountController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            if (Session["Id"] != null)
            {
                return View("Index", "Home");

            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult Login()
        {
            ViewBag.alerta = "hidden";

            //TimeZoneInfo zona = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            //DateTime elSalvador = TimeZoneInfo.ConvertTime(DateTime.Now, zona);
            //ViewBag.fecha = elSalvador.ToLongDateString();
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string contra)
        {
            
            var Consulta = (from a in db.Usuarios where a.Username == username && a.Password == contra select a).FirstOrDefault();
            if (Consulta != null)
            {
                Session["Id"] = Consulta.IdUser;
                Session["nombre"] = Consulta.Nombre;
                ViewBag.Nombre = Consulta.Nombre;
                Session["usuario"] = Consulta.Username;
                Session["roles"] = Consulta.IdRol;
                int rol = Convert.ToInt32(Session["roles"]);
                if (rol==1)
                {
                    return View("_Menu_Administrador");
                    //return RedirectToAction("Index", "DetalleCompras");
                }
                else if (rol == 2){
                    return View("_Menu_Cocinero");
                }
                else if (rol == 3)
                {
                    return View("_Menu_Mesero");
                }
                else if (rol == 4)
                {
                    return View("_Recepcion");
                }
                return RedirectToAction("Login", "Account");
            }

            ViewBag.alerta = "vissible";

            ViewBag.error = "Usuario o contraseña incorrectos";
            return View();


        }
        

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

    }
}