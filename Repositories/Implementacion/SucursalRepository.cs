using Dapper;
using SuperProjectDapperFinal.Repositories.Interface;
using SuperProjectDapperFinal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace SuperProjectDapperFinal.Repositories.Implementacion
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly IDbConnection _connectionString;

        public SucursalRepository(IDbConnection connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Sucursal> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM sucursal WHERE id_sucursal = @id_sucursal";
            return await _connectionString.QuerySingleOrDefaultAsync<Sucursal>(sql, new { id_sucursal = id });
        }

        public async Task<IEnumerable<Sucursal>> GetAllAsync()
        {
            var sql = "SELECT * FROM sucursal order by id_sucursal";
            return await _connectionString.QueryAsync<Sucursal>(sql);
        }

        public async Task<int> AddAsync(Sucursal sucursal)
        {
            var sql = @"INSERT INTO sucursal (descripcion, direccion, telefono, whatsapp, mail, estado) 
                VALUES (@descripcion, @direccion, @telefono, @whatsapp, @mail, @estado) 
                RETURNING id_sucursal";
            return await _connectionString.ExecuteScalarAsync<int>(sql, sucursal);
        }

        public async Task<bool> UpdateAsync(Sucursal sucursal)
        {
            var sql = @"UPDATE sucursal SET descripcion = @descripcion, direccion = @direccion, telefono = @telefono, whatsapp = @whatsapp, mail = @mail, estado = @estado 
                WHERE id_sucursal = @id_sucursal";
            var affectedRows = await _connectionString.ExecuteAsync(sql, sucursal);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM sucursal WHERE id_sucursal = @id_sucursal";
            var affectedRows = await _connectionString.ExecuteAsync(sql, new { id_sucursal = id });
            return affectedRows > 0;
        }
    }
}
