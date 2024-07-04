using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SuperProjectDapperFinal.Models;

public class PedidoCompraRepository : IPedidoCompraRepository
{
    private readonly IDbConnection _dbConnection;

    public PedidoCompraRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Pedido_Compra> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM pedido_compra WHERE id_pedido_compra = @Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<Pedido_Compra>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Pedido_Compra>> GetAllAsync()
    {
        var sql = "SELECT * FROM pedido_compra ORDER BY id_pedido_compra ASC";
        return await _dbConnection.QueryAsync<Pedido_Compra>(sql);
    }

    public async Task<int> AddAsync(Pedido_Compra pedidoCompra)
    {
        var sql = "INSERT INTO pedido_compra (id_proveedor, id_sucursal, fecha_hora, total) VALUES (@id_proveedor, @id_sucursal, @fecha_hora, @total) RETURNING id_pedido_compra";
        return await _dbConnection.ExecuteScalarAsync<int>(sql, pedidoCompra);
    }

    public async Task<bool> UpdateAsync(Pedido_Compra pedidoCompra)
    {
        var sql = "UPDATE pedido_compra SET id_proveedor = @id_proveedor, id_sucursal = @id_sucursal, fecha_hora = @fecha_hora, total = @total WHERE id_pedido_compra = @id_pedido_compra";
        var affectedRows = await _dbConnection.ExecuteAsync(sql, pedidoCompra);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id, IDbTransaction transaction = null)
    {
        var sql = "DELETE FROM pedido_compra WHERE id_pedido_compra = @Id";
        var affectedRows = await _dbConnection.ExecuteAsync(sql, new { Id = id }, transaction);
        return affectedRows > 0;
    }

    public async Task<IEnumerable<Detalle_Pedido>> GetDetalleByPedidoIdAsync(int pedidoCompraId)
    {
        var sql = "SELECT * FROM detalle_pedido WHERE id_pedido_compra = @PedidoCompraId";
        return await _dbConnection.QueryAsync<Detalle_Pedido>(sql, new { PedidoCompraId = pedidoCompraId });
    }

    public async Task<Pedido_Compra> GetPedidoCompraWithDetalleAsync(int id)
    {
        var sql = "SELECT * FROM pedido_compra WHERE id_pedido_compra = @Id; SELECT * FROM detalle_pedido WHERE id_pedido_compra = @Id";
        using (var multi = await _dbConnection.QueryMultipleAsync(sql, new { Id = id }))
        {
            var pedido = await multi.ReadSingleOrDefaultAsync<Pedido_Compra>();
            if (pedido != null)
            {
                pedido.Detalles = (await multi.ReadAsync<Detalle_Pedido>()).AsList();
            }
            return pedido;
        }
    }

    public async Task<decimal> CalculateTotalAsync(int pedidoCompraId)
    {
        var sql = "SELECT SUM(subtotal) FROM detalle_pedido WHERE id_pedido_compra = @PedidoCompraId";
        return await _dbConnection.ExecuteScalarAsync<decimal>(sql, new { PedidoCompraId = pedidoCompraId });
    }
}
