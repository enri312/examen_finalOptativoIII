using SuperProjectDapperFinal.Models;
using SuperProjectDapperFinal.Repositories.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DetallePedidoService : IDetallePedidoService
{
    private readonly IDetallePedidoRepository _detallePedidoRepository;
    private readonly IPedidoCompraRepository _pedidoCompraRepository;

    public DetallePedidoService(IDetallePedidoRepository detallePedidoRepository, IPedidoCompraRepository pedidoCompraRepository)
    {
        _detallePedidoRepository = detallePedidoRepository;
        _pedidoCompraRepository = pedidoCompraRepository;
    }

    public async Task<Detalle_Pedido> GetByIdAsync(int id)
    {
        return await _detallePedidoRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Detalle_Pedido>> GetAllAsync()
    {
        return await _detallePedidoRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Detalle_Pedido>> GetByPedidoIdAsync(int idPedido)
    {
        return await _detallePedidoRepository.GetByPedidoIdAsync(idPedido);
    }

    public async Task<int> AddAsync(Detalle_Pedido detallePedido)
    {
        await ValidateDetallePedido(detallePedido);
        return await _detallePedidoRepository.AddAsync(detallePedido);
    }

    public async Task<bool> UpdateAsync(Detalle_Pedido detallePedido)
    {
        await ValidateDetallePedido(detallePedido);
        return await _detallePedidoRepository.UpdateAsync(detallePedido);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _detallePedidoRepository.DeleteAsync(id);
    }

    private async Task ValidateDetallePedido(Detalle_Pedido detallePedido)
    {
        if (detallePedido.id_pedido_compra <= 0)
            throw new ArgumentException("El ID del pedido de compra es obligatorio.");
        if (detallePedido.id_producto <= 0)
            throw new ArgumentException("El ID del producto es obligatorio.");
        if (detallePedido.cantidad_producto <= 0)
            throw new ArgumentException("La cantidad del producto debe ser mayor a 0.");
        if (detallePedido.subtotal <= 0)
            throw new ArgumentException("El subtotal debe ser mayor a 0.");

        var pedidoCompra = await _pedidoCompraRepository.GetByIdAsync(detallePedido.id_pedido_compra);
        if (pedidoCompra == null)
        {
            throw new ArgumentException("El pedido de compra no existe.");
        }
    }
}
