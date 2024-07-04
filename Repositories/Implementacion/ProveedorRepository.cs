using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SuperProjectDapperFinal.Models;

namespace SuperProjectDapperFinal.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProveedorRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Proveedor>> GetAllAsync()
        {
            var sql = "SELECT * FROM proveedor order by id_proveedor";
            return await _dbConnection.QueryAsync<Proveedor>(sql);
        }

        public async Task<Proveedor> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM proveedor WHERE id_proveedor = @id_proveedor";
            return await _dbConnection.QuerySingleOrDefaultAsync<Proveedor>(sql, new { id_proveedor = id });
        }

        public async Task<int> AddAsync(Proveedor proveedor)
        {
            var sql = @"INSERT INTO proveedor (razon_social, tipo_documento, numero_documento, direccion, mail, celular, estado) 
                VALUES (@razon_social, @tipo_documento, @numero_documento, @direccion, @mail, @celular, @estado) 
                RETURNING id_proveedor";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, proveedor);
        }

        public async Task<bool> UpdateAsync(Proveedor proveedor)
        {
            var sql = @"UPDATE proveedor SET 
                        razon_social = @razon_social, 
                        tipo_documento = @tipo_documento, 
                        numero_documento = @numero_documento, 
                        direccion = @direccion, 
                        mail = @mail, 
                        celular = @celular, 
                        estado = @estado 
                        WHERE id_proveedor = @id_proveedor";
            var affectedRows = await _dbConnection.ExecuteAsync(sql, proveedor);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM proveedor WHERE id_proveedor = @id_proveedor";
            var affectedRows = await _dbConnection.ExecuteAsync(sql, new { id_proveedor = id });
            return affectedRows > 0;
        }
    }
}

