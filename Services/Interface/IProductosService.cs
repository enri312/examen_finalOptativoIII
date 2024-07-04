using System.Collections.Generic;
using System.Threading.Tasks;
using SuperProjectDapperFinal.Models;

namespace SuperProjectDapperFinal.Services
{
    public interface IProductoService
    {
        Task<Productos> GetByIdAsync(int id);
        Task<IEnumerable<Productos>> GetAllAsync();
        Task<int> AddAsync(Productos producto);
        Task<bool> UpdateAsync(Productos producto);
        Task<bool> DeleteAsync(int id);
    }
}


