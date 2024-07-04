using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperProjectDapperFinal.Models;
using SuperProjectDapperFinal.Repositories.Interface;
using SuperProjectDapperFinal.Services.Interface;

namespace SuperProjectDapperFinal.Services.Implementacion
{
    public class PedidoCompraService : IPedidoCompraService
    {
        private readonly IPedidoCompraRepository _pedidoCompraRepository;

        public PedidoCompraService(IPedidoCompraRepository pedidoCompraRepository)
        {
            _pedidoCompraRepository = pedidoCompraRepository;
        }

        public async Task<Pedido_Compra> GetByIdAsync(int id)
        {
            return await _pedidoCompraRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Pedido_Compra>> GetAllAsync()
        {
            return await _pedidoCompraRepository.GetAllAsync();
        }

        public async Task<int> AddAsync(Pedido_Compra pedidoCompra)
        {
            await ValidateAndCalculateTotalAsync(pedidoCompra);
            return await _pedidoCompraRepository.AddAsync(pedidoCompra);
        }

        public async Task<bool> UpdateAsync(Pedido_Compra pedidoCompra)
        {
            await ValidateAndCalculateTotalAsync(pedidoCompra);
            return await _pedidoCompraRepository.UpdateAsync(pedidoCompra);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _pedidoCompraRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Detalle_Pedido>> GetDetalleByPedidoIdAsync(int pedidoCompraId)
        {
            return await _pedidoCompraRepository.GetDetalleByPedidoIdAsync(pedidoCompraId);
        }

        public async Task<Pedido_Compra> GetPedidoCompraWithDetalleAsync(int id)
        {
            return await _pedidoCompraRepository.GetPedidoCompraWithDetalleAsync(id);
        }

        private async Task ValidateAndCalculateTotalAsync(Pedido_Compra pedidoCompra)
        {
            if (pedidoCompra.id_proveedor <= 0)
                throw new ArgumentException("El ID del proveedor es obligatorio.");
            if (pedidoCompra.id_sucursal <= 0)
                throw new ArgumentException("El ID de la sucursal es obligatorio.");

            var detalles = await _pedidoCompraRepository.GetDetalleByPedidoIdAsync(pedidoCompra.id_pedido_compra);
            decimal total = 0;
            foreach (var detalle in detalles)
            {
                total += detalle.subtotal;
            }
            if (total <= 0)
                throw new ArgumentException("El total debe ser mayor a 0.");
            pedidoCompra.total = total;
        }
    }
}
