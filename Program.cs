using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperProjectDapperFinal.Models;
//using SuperProjectDapperFinal.Repositories.Data;
using SuperProjectDapperFinal.Repositories.Implementacion;
using SuperProjectDapperFinal.Repositories.Interface;
using SuperProjectDapperFinal.Services.Implementacion;
using SuperProjectDapperFinal.Services.Interface;

namespace SuperProjectDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //var host = CreateHostBuilder(args).Build();

            //var productoService = host.Services.GetRequiredService<IProductoService>();
            //var clienteService = host.Services.GetRequiredService<IClienteService>();
            //var sucursalService = host.Services.GetRequiredService<ISucursalService>();
            //var facturaService = host.Services.GetRequiredService<IFacturaService>();
            //var detalleFacturaService = host.Services.GetRequiredService<IDetalleFacturaService>();

            var producto = new Productos
            {
                descripcion = "Nuevo Producto",
                cantidad_minima = 5,
                cantidad_stock = 50,
                precio_compra = 100,
                precio_venta = 150,
                categoria = "Categoría A",
                marca = "Marca A",
                estado = true
            };
            //productoService.AggProducto(producto);

            /* var cliente = new Cliente
             {
                 Id_banco = 1,
                 Nombre = "Juan",
                 Apellido = "Perez",
                 Documento = "12345678",
                 Direccion = "Av. Siempre Viva 123",
                 Mail = "juan.perez@example.com",
                 Celular = "1234567890",
                 Estado = true
             };*/
            /*clienteService.AggCliente(cliente);

            var sucursal = new Sucursal
            {
                Descripcion = "Sucursal Central",
                Direccion = "Calle Falsa 123",
                Telefono = "1234567",
                Whatsapp = "7654321",
                Mail = "sucursal@empresa.com",
                Estado = true
            };
            sucursalService.AddSucursal(sucursal);

            var factura = new Factura
            {
                IdCliente = cliente.Id_cliente,
                IdSucursal = sucursal.IdSucursal,
                NroFactura = "001-001-000001",
                FechaHora = DateTime.Now,
                Total = 1000m,
                TotalIva5 = 50m,
                TotalIva10 = 100m,
                TotalIva = 150m,
                TotalLetras = "Mil pesos"
            };
            facturaService.AddFactura(factura);

            var detalleFactura = new DetalleFactura
            {
                IdFactura = factura.IdFactura,
                IdProducto = producto.id_productos,
                CantidadProducto = 2,
                Subtotal = 300m
            };
            detalleFacturaService.AddDetalle(detalleFactura);

            Console.WriteLine("Producto, cliente, sucursal, factura y detalle de factura agregados exitosamente.");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<DBConnections>();
                    services.AddScoped(sp => sp.GetRequiredService<DBConnections>().CreateConnection());

                    // Repositorios y servicios de Producto
                    services.AddScoped<IProductoRepository, ProductoRepository>();
                    services.AddScoped<IProductoService, ProductoService>();

                    // Repositorios y servicios de Cliente
                    services.AddScoped<IClienteRepository, ClienteRepository>();
                    services.AddScoped<IClienteService, ClienteService>();

                    // Repositorios y servicios de Sucursal
                    services.AddScoped<ISucursalRepository, SucursalRepository>();
                    services.AddScoped<ISucursalService, SucursalService>();

                    // Repositorios y servicios de Factura
                    services.AddScoped<IFacturaRepository, FacturaRepository>();
                    services.AddScoped<IFacturaService, FacturaService>();

                    // Repositorios y servicios de DetalleFactura
                    services.AddScoped<IDetalleFacturaRepository, DetalleFacturaRepository>();
                    services.AddScoped<IDetalleFacturaService, DetalleFacturaService>();

                    // Agregar otros repositorios y servicios aquí según sea necesario
                });
    }*/
        }
    }
}
