using System.Collections.Generic;
using System.Threading.Tasks;
using SuperProjectDapperFinal.Models;

namespace SuperProjectDapperFinal.Services
{
    public interface IProveedorService
    {
        Task<Proveedor> GetByIdAsync(int id);
        Task<IEnumerable<Proveedor>> GetAllAsync();
        Task<int> AddAsync(Proveedor proveedor);
        Task<bool> UpdateAsync(Proveedor proveedor);
        Task<bool> DeleteAsync(int id);
    }
}

