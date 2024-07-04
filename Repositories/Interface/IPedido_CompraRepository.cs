using SuperProjectDapperFinal.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public interface IPedidoCompraRepository
{
    Task<Pedido_Compra> GetByIdAsync(int id);
    Task<IEnumerable<Pedido_Compra>> GetAllAsync();
    Task<int> AddAsync(Pedido_Compra pedidoCompra);
    Task<bool> UpdateAsync(Pedido_Compra pedidoCompra);
    Task<bool> DeleteAsync(int id, IDbTransaction transaction = null);
    Task<IEnumerable<Detalle_Pedido>> GetDetalleByPedidoIdAsync(int pedidoCompraId);
    Task<Pedido_Compra> GetPedidoCompraWithDetalleAsync(int id);
    Task<decimal> CalculateTotalAsync(int pedidoCompraId);
}
