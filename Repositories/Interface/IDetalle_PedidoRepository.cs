using SuperProjectDapperFinal.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public interface IDetallePedidoRepository
{
    Task<Detalle_Pedido> GetByIdAsync(int id);
    Task<IEnumerable<Detalle_Pedido>> GetAllAsync();
    Task<int> AddAsync(Detalle_Pedido detallePedido);
    Task<bool> UpdateAsync(Detalle_Pedido detallePedido);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteByPedidoCompraIdAsync(int pedidoCompraId, IDbTransaction transaction = null);
    Task<IEnumerable<Detalle_Pedido>> GetByPedidoIdAsync(int idPedido);
}
