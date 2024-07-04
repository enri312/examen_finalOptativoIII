using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SuperProjectDapperFinal.Models;

namespace SuperProjectDapperFinal.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Productos>> GetAllAsync()
        {
            var sql = "SELECT * FROM productos order by id_producto";
            return await _dbConnection.QueryAsync<Productos>(sql);
        }

        public async Task<Productos> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM productos WHERE id_producto = @id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Productos>(sql, new { id });
        }

        public async Task<int> AddAsync(Productos producto)
        {
            var sql = @"INSERT INTO productos (descripcion, cantidad_minima, cantidad_stock, precio_compra, precio_venta, categoria, marca, estado) 
                        VALUES (@descripcion, @cantidad_minima, @cantidad_stock, @precio_compra, @precio_venta, @categoria, @marca, @estado)
                        RETURNING id_producto";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, producto);
        }

        public async Task<bool> UpdateAsync(Productos producto)
        {
            var sql = @"UPDATE productos SET 
                        descripcion = @descripcion, 
                        cantidad_minima = @cantidad_minima, 
                        cantidad_stock = @cantidad_stock, 
                        precio_compra = @precio_compra, 
                        precio_venta = @precio_venta, 
                        categoria = @categoria, 
                        marca = @marca, 
                        estado = @estado 
                        WHERE id_producto = @id_producto";
            var affectedRows = await _dbConnection.ExecuteAsync(sql, producto);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM productos WHERE id_producto = @id";
            var affectedRows = await _dbConnection.ExecuteAsync(sql, new { id });
            return affectedRows > 0;
        }
    }
}
