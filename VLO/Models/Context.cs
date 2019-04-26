using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class Context : DbContext
    {
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<DetalleCompra> DetalleCompra { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Receta> Receta { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Turnos> Turnos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<TipoMenu> TipoMenus { get; set; }
        public DbSet<AsignacionTurno> AsignacionTurno { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
    }
}