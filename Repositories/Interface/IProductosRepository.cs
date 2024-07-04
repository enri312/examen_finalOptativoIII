using SuperProjectDapperFinal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperProjectDapperFinal.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Productos>> GetAllAsync();
        Task<Productos> GetByIdAsync(int id);
        Task<int> AddAsync(Productos producto);
        Task<bool> UpdateAsync(Productos producto);
        Task<bool> DeleteAsync(int id);
    }
}



