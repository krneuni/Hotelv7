namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AsignacionTurnoes",
                c => new
                    {
                        IdAsignacion = c.Int(nullable: false, identity: true),
                        IdTurno = c.Int(nullable: false),
                        Fecha = c.String(nullable: false),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAsignacion)
                .ForeignKey("dbo.Turnos", t => t.IdTurno, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdTurno)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Turnos",
                c => new
                    {
                        IdTurno = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25),
                        HoraInicial = c.String(nullable: false),
                        HoraFinal = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdTurno);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        IdRol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdUser)
                .ForeignKey("dbo.Roles", t => t.IdRol, cascadeDelete: true)
                .Index(t => t.IdRol);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        IdPedido = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        Cliente = c.String(nullable: false, maxLength: 75),
                        Estado = c.Int(nullable: false),
                        IdMesa = c.Int(nullable: false),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPedido)
                .ForeignKey("dbo.Mesas", t => t.IdMesa, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdMesa)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Mesas",
                c => new
                    {
                        IdMesa = c.Int(nullable: false, identity: true),
                        NumMesa = c.String(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdMesa);
            
            CreateTable(
                "dbo.Prestamos",
                c => new
                    {
                        IdPrestamo = c.Int(nullable: false, identity: true),
                        Cantidad = c.Double(nullable: false),
                        CantidadDevuelta = c.Double(nullable: false),
                        Fecha = c.String(),
                        Estado = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPrestamo)
                .ForeignKey("dbo.Productos", t => t.IdProducto, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdProducto)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        IdProducto = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 75),
                        Marca = c.String(maxLength: 75),
                        Cantidad = c.Double(nullable: false),
                        Fecha = c.String(),
                        CantidadMinima = c.Double(nullable: false),
                        UnidadMedida = c.String(nullable: false, maxLength: 25),
                        Estado = c.Boolean(nullable: false),
                        IdCategoria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProducto)
                .ForeignKey("dbo.Categorias", t => t.IdCategoria, cascadeDelete: true)
                .Index(t => t.IdCategoria);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        IdCategoria = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 75),
                    })
                .PrimaryKey(t => t.IdCategoria);
            
            CreateTable(
                "dbo.DetalleCompras",
                c => new
                    {
                        IdDetalle = c.Int(nullable: false, identity: true),
                        Cantidad = c.Double(nullable: false),
                        PrecioUnit = c.Double(nullable: false),
                        FechaCompra = c.String(nullable: false),
                        PrecioTotal = c.Double(nullable: false),
                        Codigo = c.String(nullable: false),
                        IdProveedor = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDetalle)
                .ForeignKey("dbo.Productos", t => t.IdProducto, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.IdProveedor, cascadeDelete: true)
                .Index(t => t.IdProveedor)
                .Index(t => t.IdProducto);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        IdProveedor = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 75),
                        Direccion = c.String(maxLength: 150),
                        Telefono = c.String(nullable: false),
                        Mail = c.String(),
                        NombreContacto = c.String(nullable: false, maxLength: 75),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdProveedor);
            
            CreateTable(
                "dbo.Recetas",
                c => new
                    {
                        IdReceta = c.Int(nullable: false, identity: true),
                        CantidadUtilizada = c.Double(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        IdMenu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdReceta)
                .ForeignKey("dbo.Menus", t => t.IdMenu, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdProducto)
                .Index(t => t.IdMenu);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        IdMenu = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 75),
                        Precio = c.Double(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        IdTipoMenu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdMenu)
                .ForeignKey("dbo.TipoMenus", t => t.IdTipoMenu, cascadeDelete: true)
                .Index(t => t.IdTipoMenu);
            
            CreateTable(
                "dbo.DetallePedidoes",
                c => new
                    {
                        IdDetalle = c.Int(nullable: false, identity: true),
                        IdMenu = c.Int(nullable: false),
                        IdPedido = c.Int(nullable: false),
                        cantidad = c.Int(nullable: false),
                        sesion = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                        Termino = c.String(),
                    })
                .PrimaryKey(t => t.IdDetalle)
                .ForeignKey("dbo.Menus", t => t.IdMenu, cascadeDelete: true)
                .ForeignKey("dbo.Pedidoes", t => t.IdPedido, cascadeDelete: true)
                .Index(t => t.IdMenu)
                .Index(t => t.IdPedido);
            
            CreateTable(
                "dbo.TipoMenus",
                c => new
                    {
                        IdTipoMenu = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 75),
                    })
                .PrimaryKey(t => t.IdTipoMenu);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        IdRol = c.Int(nullable: false, identity: true),
                        Tipo = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdRol);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        NumFactura = c.Int(nullable: false, identity: true),
                        IdDetalle = c.Int(nullable: false),
                        Precio = c.Double(nullable: false),
                        Descuento = c.Double(nullable: false),
                        FechaFactura = c.String(),
                        Propina = c.Double(nullable: false),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.NumFactura)
                .ForeignKey("dbo.DetallePedidoes", t => t.IdDetalle, cascadeDelete: true)
                .Index(t => t.IdDetalle);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facturas", "IdDetalle", "dbo.DetallePedidoes");
            DropForeignKey("dbo.Usuarios", "IdRol", "dbo.Roles");
            DropForeignKey("dbo.Prestamos", "IdUser", "dbo.Usuarios");
            DropForeignKey("dbo.Recetas", "IdProducto", "dbo.Productos");
            DropForeignKey("dbo.Recetas", "IdMenu", "dbo.Menus");
            DropForeignKey("dbo.Menus", "IdTipoMenu", "dbo.TipoMenus");
            DropForeignKey("dbo.DetallePedidoes", "IdPedido", "dbo.Pedidoes");
            DropForeignKey("dbo.DetallePedidoes", "IdMenu", "dbo.Menus");
            DropForeignKey("dbo.Prestamos", "IdProducto", "dbo.Productos");
            DropForeignKey("dbo.DetalleCompras", "IdProveedor", "dbo.Proveedores");
            DropForeignKey("dbo.DetalleCompras", "IdProducto", "dbo.Productos");
            DropForeignKey("dbo.Productos", "IdCategoria", "dbo.Categorias");
            DropForeignKey("dbo.Pedidoes", "IdUser", "dbo.Usuarios");
            DropForeignKey("dbo.Pedidoes", "IdMesa", "dbo.Mesas");
            DropForeignKey("dbo.AsignacionTurnoes", "IdUser", "dbo.Usuarios");
            DropForeignKey("dbo.AsignacionTurnoes", "IdTurno", "dbo.Turnos");
            DropIndex("dbo.Facturas", new[] { "IdDetalle" });
            DropIndex("dbo.DetallePedidoes", new[] { "IdPedido" });
            DropIndex("dbo.DetallePedidoes", new[] { "IdMenu" });
            DropIndex("dbo.Menus", new[] { "IdTipoMenu" });
            DropIndex("dbo.Recetas", new[] { "IdMenu" });
            DropIndex("dbo.Recetas", new[] { "IdProducto" });
            DropIndex("dbo.DetalleCompras", new[] { "IdProducto" });
            DropIndex("dbo.DetalleCompras", new[] { "IdProveedor" });
            DropIndex("dbo.Productos", new[] { "IdCategoria" });
            DropIndex("dbo.Prestamos", new[] { "IdUser" });
            DropIndex("dbo.Prestamos", new[] { "IdProducto" });
            DropIndex("dbo.Pedidoes", new[] { "IdUser" });
            DropIndex("dbo.Pedidoes", new[] { "IdMesa" });
            DropIndex("dbo.Usuarios", new[] { "IdRol" });
            DropIndex("dbo.AsignacionTurnoes", new[] { "IdUser" });
            DropIndex("dbo.AsignacionTurnoes", new[] { "IdTurno" });
            DropTable("dbo.Facturas");
            DropTable("dbo.Roles");
            DropTable("dbo.TipoMenus");
            DropTable("dbo.DetallePedidoes");
            DropTable("dbo.Menus");
            DropTable("dbo.Recetas");
            DropTable("dbo.Proveedores");
            DropTable("dbo.DetalleCompras");
            DropTable("dbo.Categorias");
            DropTable("dbo.Productos");
            DropTable("dbo.Prestamos");
            DropTable("dbo.Mesas");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Turnos");
            DropTable("dbo.AsignacionTurnoes");
        }
    }
}
