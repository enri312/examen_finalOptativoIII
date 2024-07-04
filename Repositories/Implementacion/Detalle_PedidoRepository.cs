using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SuperProjectDapperFinal.Models;

public class DetallePedidoRepository : IDetallePedidoRepository
{
    private readonly IDbConnection _dbConnection;

    public DetallePedidoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Detalle_Pedido> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM detalle_pedido WHERE id_detalle_pedido = @Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<Detalle_Pedido>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Detalle_Pedido>> GetAllAsync()
    {
        var sql = "SELECT * FROM detalle_pedido ORDER BY id_detalle_pedido ASC";
        return await _dbConnection.QueryAsync<Detalle_Pedido>(sql);
    }

    public async Task<IEnumerable<Detalle_Pedido>> GetByPedidoIdAsync(int idPedido)
    {
        var sql = "SELECT * FROM detalle_pedido WHERE id_pedido_compra = @id_pedido_compra";
        return await _dbConnection.QueryAsync<Detalle_Pedido>(sql, new { id_pedido_compra = idPedido });
    }

    public async Task<int> AddAsync(Detalle_Pedido detallePedido)
    {
        var sql = "INSERT INTO detalle_pedido (id_pedido_compra, id_producto, cantidad_producto, subtotal) VALUES (@id_pedido_compra, @id_producto, @cantidad_producto, @subtotal) RETURNING id_detalle_pedido";
        return await _dbConnection.ExecuteScalarAsync<int>(sql, detallePedido);
    }

    public async Task<bool> UpdateAsync(Detalle_Pedido detallePedido)
    {
        var sql = "UPDATE detalle_pedido SET id_pedido_compra = @id_pedido_compra, id_producto = @id_producto, cantidad_producto = @cantidad_producto, subtotal = @subtotal WHERE id_detalle_pedido = @id_detalle_pedido";
        var affectedRows = await _dbConnection.ExecuteAsync(sql, detallePedido);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sql = "DELETE FROM detalle_pedido WHERE id_detalle_pedido = @Id";
        var affectedRows = await _dbConnection.ExecuteAsync(sql, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<bool> DeleteByPedidoCompraIdAsync(int pedidoCompraId, IDbTransaction transaction = null)
    {
        var sql = "DELETE FROM detalle_pedido WHERE id_pedido_compra = @PedidoCompraId";
        var affectedRows = await _dbConnection.ExecuteAsync(sql, new { PedidoCompraId = pedidoCompraId }, transaction);
        return affectedRows > 0;
    }
}
