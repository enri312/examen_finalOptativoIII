using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using SuperProjectDapperFinal.Models;
using SuperProjectDapperFinal.Repositories;
using SuperProjectDapperFinal.Repositories.Implementacion;
using SuperProjectDapperFinal.Repositories.Interface;
using SuperProjectDapperFinal.Services;
using SuperProjectDapperFinal.Services.Implementacion;
using SuperProjectDapperFinal.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace SuperProjectDapperFinal.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var serviceProvider = host.Services;

            while (true)
            {
                Console.WriteLine("Seleccione la entidad a probar:");
                Console.WriteLine("1. Producto");
                Console.WriteLine("2. Proveedor");
                Console.WriteLine("3. Sucursal");
                Console.WriteLine("4. Pedido Compra");
                Console.WriteLine("5. Detalle Pedido Compra");
                Console.WriteLine("0. Salir\n");
                var opcion = Console.ReadLine();

                if (opcion == "0")
                {
                    break;
                }

                switch (opcion)
                {
                    case "1":
                        await MostrarMenuEntidad(serviceProvider, "Producto", ProbarProducto);
                        break;
                    case "2":
                        await MostrarMenuEntidad(serviceProvider, "Proveedor", ProbarProveedor);
                        break;
                    case "3":
                        await MostrarMenuEntidad(serviceProvider, "Sucursal", ProbarSucursal);
                        break;
                    case "4":
                        await MostrarMenuEntidad(serviceProvider, "Pedido Compra", ProbarPedidoCompra);
                        break;
                    case "5":
                        await MostrarMenuEntidad(serviceProvider, "Detalle Pedido Compra", ProbarDetallePedidoCompra);
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                    services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(connectionString));

                    // Producto
                    services.AddTransient<IProductoRepository, ProductoRepository>();
                    services.AddTransient<IProductoService, ProductoService>();

                    // Proveedor
                    services.AddTransient<IProveedorRepository, ProveedorRepository>();
                    services.AddTransient<IProveedorService, ProveedorService>();

                    // Sucursal
                    services.AddTransient<ISucursalRepository, SucursalRepository>();
                    services.AddTransient<ISucursalService, SucursalService>();

                    // Pedido Compra
                    services.AddTransient<IPedidoCompraRepository, PedidoCompraRepository>();
                    services.AddTransient<IPedidoCompraService, PedidoCompraService>();

                    // Detalle Pedido Compra
                    services.AddTransient<IDetallePedidoRepository, DetallePedidoRepository>();
                    services.AddTransient<IDetallePedidoService, DetallePedidoService>();
                });

        static async Task MostrarMenuEntidad(IServiceProvider serviceProvider, string entidad, Func<IServiceProvider, string, Task> entidadFuncion)
        {
            while (true)
            {
                Console.WriteLine($"\nSeleccione una operación para {entidad}:");
                Console.WriteLine("1. Insertar nuevo registro");
                Console.WriteLine("2. Actualizar registro existente");
                Console.WriteLine("3. Eliminar registro");
                Console.WriteLine("4. Seleccionar registros");
                Console.WriteLine("0. Volver al menú principal\n");
                var operacion = Console.ReadLine();

                if (operacion == "0")
                {
                    break;
                }

                switch (operacion)
                {
                    case "1":
                        await entidadFuncion(serviceProvider, "Insertar");
                        break;
                    case "2":
                        await entidadFuncion(serviceProvider, "Actualizar");
                        break;
                    case "3":
                        await entidadFuncion(serviceProvider, "Eliminar");
                        break;
                    case "4":
                        await entidadFuncion(serviceProvider, "Seleccionar");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static async Task ProbarProducto(IServiceProvider serviceProvider, string operacion)
        {
            var productoService = serviceProvider.GetRequiredService<IProductoService>();

            switch (operacion)
            {
                case "Insertar":
                    var nuevoProducto = new Productos
                    {
                       /* descripcion = "Leche",
                        cantidad_minima = 10,
                        cantidad_stock = 50,
                        precio_compra = 3000,
                        precio_venta = 3500,
                        categoria = "Lácteos",
                        marca = "Trébol",
                        estado = true*/

                        /*descripcion = "Queso",
                        cantidad_minima = 5,
                        cantidad_stock = 100,
                        precio_compra = 4000,
                        precio_venta = 4500,
                        categoria = "Lácteos",
                        marca = "Trébol",
                        estado = true*/

                        descripcion = "Manteca",
                        cantidad_minima = 3,
                        cantidad_stock = 30,
                        precio_compra = 4500,
                        precio_venta = 5000,
                        categoria = "Lácteos",
                        marca = "Trébol",
                        estado = true

                    };
                    int nuevoProductoId = await productoService.AddAsync(nuevoProducto);
                    Console.WriteLine($"\nSe ha insertado correctamente el producto con Id: {nuevoProductoId}\n");
                    break;

                case "Actualizar":
                    Console.WriteLine("\nIngrese el ID del producto a actualizar:\n");
                    int idProductoActualizar = int.Parse(Console.ReadLine());
                    var productoParaActualizar = await productoService.GetByIdAsync(idProductoActualizar);
                    productoParaActualizar.marca = "Lactolanda";
                    bool isUpdated = await productoService.UpdateAsync(productoParaActualizar);
                    Console.WriteLine($"\nProducto Actualizado: {isUpdated}\n");
                    break;

                case "Eliminar":
                    Console.WriteLine("\nIngrese el ID del producto a eliminar:");
                    int idProductoEliminar = int.Parse(Console.ReadLine());
                    bool isDeleted = await productoService.DeleteAsync(idProductoEliminar);
                    Console.WriteLine($"\nProducto Eliminado: {isDeleted}\n");
                    break;

                case "Seleccionar":
                    var productos = await productoService.GetAllAsync();
                    if (productos.Any())
                    {
                        foreach (var producto in productos)
                        {
                            Console.WriteLine($"\nID: {producto.id_producto}, Descripción: {producto.descripcion}, Cantidad Mínima: {producto.cantidad_minima}, Cantidad Stock: {producto.cantidad_stock}, Precio Compra: {producto.precio_compra}, Precio Venta: {producto.precio_venta}, Categoría: {producto.categoria}, Marca: {producto.marca}, Estado: {producto.estado}\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo hay registros para mostrar.\n");
                    }
                    break;
            }
        }

        static async Task ProbarProveedor(IServiceProvider serviceProvider, string operacion)
        {
            var proveedorService = serviceProvider.GetRequiredService<IProveedorService>();

            switch (operacion)
            {
                case "Insertar":
                    var nuevoProveedor = new Proveedor
                    {
                        /*razon_social = "Proveedor Zeus",
                        tipo_documento = "C.I",
                        numero_documento = "12345678",
                        direccion = "Dirección 1",
                        mail = "proveedorzeus@example.com",
                        celular = "9876543210",
                        estado = true*/

                        /*razon_social = "Proveedor Artemisa",
                        tipo_documento = "C.I",
                        numero_documento = "87654321",
                        direccion = "Dirección 2",
                        mail = "proveedorartms@example.com",
                        celular = "9876523215",
                        estado = true*/

                        razon_social = "Proveedor Ares",
                        tipo_documento = "C.I",
                        numero_documento = "27624354",
                        direccion = "Dirección 3",
                        mail = "proveedorares@example.com",
                        celular = "9846521275",
                        estado = true
                    };
                    int nuevoProveedorId = await proveedorService.AddAsync(nuevoProveedor);
                    Console.WriteLine($"\nSe ha insertado correctamente el proveedor con Id: {nuevoProveedorId}\n");
                    break;

                case "Actualizar":
                    Console.WriteLine("\nIngrese el ID del proveedor a actualizar:\n");
                    int idProveedorActualizar = int.Parse(Console.ReadLine());
                    var proveedorParaActualizar = await proveedorService.GetByIdAsync(idProveedorActualizar);
                    proveedorParaActualizar.razon_social = "Proveedor Hades";
                    bool isUpdated = await proveedorService.UpdateAsync(proveedorParaActualizar);
                    Console.WriteLine($"\nProveedor Actualizado: {isUpdated}\n");
                    break;

                case "Eliminar":
                    Console.WriteLine("\nIngrese el ID del proveedor a eliminar:\n");
                    int idProveedorEliminar = int.Parse(Console.ReadLine());
                    bool isDeleted = await proveedorService.DeleteAsync(idProveedorEliminar);
                    Console.WriteLine($"\nProveedor Eliminado: {isDeleted}\n");
                    break;

                case "Seleccionar":
                    var proveedores = await proveedorService.GetAllAsync();
                    if (proveedores.Any())
                    {
                        foreach (var proveedor in proveedores)
                        {
                            Console.WriteLine($"\nID: {proveedor.id_proveedor}, Razón Social: {proveedor.razon_social}, Tipo Documento: {proveedor.tipo_documento}, Número Documento: {proveedor.numero_documento}, Dirección: {proveedor.direccion}, Mail: {proveedor.mail}, Celular: {proveedor.celular}, Estado: {proveedor.estado}\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo hay registros para mostrar.\n");
                    }
                    break;
                    
            }
        }

        static async Task ProbarSucursal(IServiceProvider serviceProvider, string operacion)
        {
            var sucursalService = serviceProvider.GetRequiredService<ISucursalService>();

            switch (operacion)
            {
                case "Insertar":
                    var nuevaSucursal = new Sucursal
                    {
                        /*descripcion = "Sucursal Olimpo",
                        direccion = "Calle Principal Olimpo 123456",
                        telefono = "1234567890",
                        whatsapp = "9876543211",
                        mail = "contacto@sucursal.com",
                        estado = true*/

                        /*descripcion = "Sucursal Inframundo",
                        direccion = "Calle Principal Inframundo 652562",
                        telefono = "9876543214",
                        whatsapp = "1122334555",
                        mail = "contacto2@sucursal.com",
                        estado = true*/

                        descripcion = "Sucursal Atenas",
                        direccion = "Calle Principal Atena 432234",
                        telefono = "1278563213",
                        whatsapp = "9876545567",
                        mail = "contacto3@sucursal.com",
                        estado = true
                    };

                    int idSucursal = await sucursalService.AddAsync(nuevaSucursal);
                    Console.WriteLine($"\nNueva Sucursal creada con ID: {idSucursal}\n");
                    break;

                case "Actualizar":
                    Console.WriteLine("\nIngrese el ID de la sucursal a actualizar:\n");
                    int idSucursalActualizar = int.Parse(Console.ReadLine());
                    var sucursalParaActualizar = await sucursalService.GetByIdAsync(idSucursalActualizar);
                    sucursalParaActualizar.direccion = "Avenida Secundaria 456789";
                    await sucursalService.UpdateAsync(sucursalParaActualizar);
                    Console.WriteLine("\nSucursal actualizada.\n");
                    break;

                case "Eliminar":
                    Console.WriteLine("\nIngrese el ID de la sucursal a eliminar:\n");
                    int idSucursalEliminar = int.Parse(Console.ReadLine());
                    bool isDeleted = await sucursalService.DeleteAsync(idSucursalEliminar);
                    Console.WriteLine($"\nSucursal Eliminada: {isDeleted}\n");
                    break;

                case "Seleccionar":
                    var sucursales = await sucursalService.GetAllAsync();
                    if (sucursales.Any())
                    {
                        foreach (var sucursal in sucursales)
                        {
                            Console.WriteLine($"\nID: {sucursal.id_sucursal}, Descripción: {sucursal.descripcion}, Dirección: {sucursal.direccion}, Teléfono: {sucursal.telefono}, Whatsapp: {sucursal.whatsapp}, Mail: {sucursal.mail}, Estado: {sucursal.estado}\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo hay registros para mostrar.\n");
                    }
                    break;
            }
        }

        static async Task ProbarPedidoCompra(IServiceProvider serviceProvider, string operacion)
        {
            var pedidoService = serviceProvider.GetRequiredService<IPedidoCompraService>();

            switch (operacion)
            {
                case "Insertar":
                    var nuevoPedido = new Pedido_Compra
                    {
                        id_proveedor = 1,
                        id_sucursal = 1,
                        fecha_hora = DateTime.Now,
                        Detalles = new List<Detalle_Pedido>
                        {
                            new Detalle_Pedido { id_producto = 1, cantidad_producto = 10, subtotal = 35000 },
                            new Detalle_Pedido { id_producto = 2, cantidad_producto = 5, subtotal = 22500 }
                        }

                        /* id_proveedor = 2,
                         id_sucursal = 2,
                         fecha_hora = DateTime.Now,
                         Detalles = new List<Detalle_Pedido>
                         {
                             new Detalle_Pedido { id_producto = 2, cantidad_producto = 5, subtotal = 22500 },
                             new Detalle_Pedido { id_producto = 3, cantidad_producto = 3, subtotal = 15000 }
                         }

                         id_proveedor = 3,
                         id_sucursal = 3,
                         fecha_hora = DateTime.Now,
                         Detalles = new List<Detalle_Pedido>
                         {
                             new Detalle_Pedido { id_producto = 1, cantidad_producto = 10, subtotal = 35000 },
                             new Detalle_Pedido { id_producto = 3, cantidad_producto = 3, subtotal = 15000 }
                         }*/
                    };
                    int nuevoPedidoId = await pedidoService.AddAsync(nuevoPedido);
                    Console.WriteLine($"\nSe ha insertado correctamente el pedido con Id: {nuevoPedidoId}\n");
                    break;

                case "Actualizar":
                    Console.WriteLine("\nIngrese el ID del pedido a actualizar:\n");
                    int idPedidoActualizar = int.Parse(Console.ReadLine());
                    var pedidoParaActualizar = await pedidoService.GetByIdAsync(idPedidoActualizar);
                    pedidoParaActualizar.fecha_hora = DateTime.Now.AddDays(1); //aqui actualizamos mi fecha_hora
                    bool isUpdated = await pedidoService.UpdateAsync(pedidoParaActualizar);
                    Console.WriteLine($"\nPedido Actualizado: {isUpdated}\n");
                    break;

                case "Eliminar":
                    Console.WriteLine("\nIngrese el ID del pedido a eliminar:\n");
                    int idPedidoEliminar = int.Parse(Console.ReadLine());
                    bool isDeleted = await pedidoService.DeleteAsync(idPedidoEliminar);
                    Console.WriteLine($"\nPedido Eliminado: {isDeleted}\n");
                    break;

                case "Seleccionar":
                    var pedidos = await pedidoService.GetAllAsync();
                    if (pedidos.Any())
                    {
                        foreach (var pedido in pedidos)
                        {
                            Console.WriteLine($"\nID: {pedido.id_pedido_compra}, Proveedor ID: {pedido.id_proveedor}, Sucursal ID: {pedido.id_sucursal}, Fecha Pedido: {pedido.fecha_hora},  Total: {pedido.total}\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo hay registros para mostrar.\n");
                    }
                    break;
            }
        }

        static async Task ProbarDetallePedidoCompra(IServiceProvider serviceProvider, string operacion)
        {
            var detalleService = serviceProvider.GetRequiredService<IDetallePedidoService>();

            switch (operacion)
            {
                case "Insertar":
                    var nuevoDetalle = new Detalle_Pedido
                    {
                        id_pedido_compra = 1,
                        id_producto = 1,
                        cantidad_producto = 5,
                        subtotal = 5000

                        /* id_pedido_compra = 1,
                         id_producto = 2,
                         cantidad_producto = 5,
                         subtotal = 5000*/
                    };
                    int nuevoDetalleId = await detalleService.AddAsync(nuevoDetalle);
                    Console.WriteLine($"\nSe ha insertado correctamente el detalle del pedido con Id: {nuevoDetalleId}\n");
                    break;

                case "Actualizar":
                    Console.WriteLine("\nIngrese el ID del detalle del pedido a actualizar:\n");
                    int idDetalleActualizar = int.Parse(Console.ReadLine());
                    var detalleParaActualizar = await detalleService.GetByIdAsync(idDetalleActualizar);
                    detalleParaActualizar.cantidad_producto = 10;
                    bool isUpdated = await detalleService.UpdateAsync(detalleParaActualizar);
                    Console.WriteLine($"\nDetalle del Pedido Actualizado: {isUpdated}\n");
                    break;

                case "Eliminar":
                    Console.WriteLine("\nIngrese el ID del detalle del pedido a eliminar:\n");
                    int idDetalleEliminar = int.Parse(Console.ReadLine());
                    bool isDeleted = await detalleService.DeleteAsync(idDetalleEliminar);
                    Console.WriteLine($"\nDetalle del Pedido Eliminado: {isDeleted}\n");
                    break;

                case "Seleccionar":
                    var detalles = await detalleService.GetAllAsync();
                    if (detalles.Any())
                    {
                        foreach (var detalle in detalles)
                        {
                            Console.WriteLine($"\nID: {detalle.id_detalle_pedido}, Pedido ID: {detalle.id_pedido_compra}, Producto ID: {detalle.id_producto}, Cantidad: {detalle.cantidad_producto}, Precio Compra: {detalle.subtotal}\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo hay registros para mostrar.\n");
                    }
                    break;
            }
        }
    }
}
