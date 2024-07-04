using System.Collections.Generic;
using System.Threading.Tasks;
using SuperProjectDapperFinal.Models;

namespace SuperProjectDapperFinal.Services.Interface
{
    public interface ISucursalService
    {
        Task<Sucursal> GetByIdAsync(int id);
        Task<IEnumerable<Sucursal>> GetAllAsync();
        Task<int> AddAsync(Sucursal sucursal);
        Task<bool> UpdateAsync(Sucursal sucursal);
        Task<bool> DeleteAsync(int id);
    }
}
