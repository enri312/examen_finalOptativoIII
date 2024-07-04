using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperProjectDapperFinal.Models;
using SuperProjectDapperFinal.Repositories;

namespace SuperProjectDapperFinal.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Productos> GetByIdAsync(int id)
        {
            return await _productoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Productos>> GetAllAsync()
        {
            return await _productoRepository.GetAllAsync();
        }

        public async Task<int> AddAsync(Productos producto)
        {
            ValidateProducto(producto);
            return await _productoRepository.AddAsync(producto);
        }

        public async Task<bool> UpdateAsync(Productos producto)
        {
            ValidateProducto(producto);
            return await _productoRepository.UpdateAsync(producto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productoRepository.DeleteAsync(id);
        }

        private void ValidateProducto(Productos producto)
        {
            if (string.IsNullOrWhiteSpace(producto.descripcion))
                throw new ArgumentException("La descripción es obligatoria.");
            if (producto.cantidad_minima <= 1)
                throw new ArgumentException("La cantidad mínima debe ser mayor a 1.");
            if (producto.cantidad_stock < 0)
                throw new ArgumentException("La cantidad de stock no puede ser negativa.");
            if (producto.precio_compra <= 0 || producto.precio_venta <= 0)
                throw new ArgumentException("El precio de compra y el precio de venta deben ser enteros positivos.");
            if (string.IsNullOrWhiteSpace(producto.categoria))
                throw new ArgumentException("La categoría es obligatoria.");
            if (string.IsNullOrWhiteSpace(producto.marca))
                throw new ArgumentException("La marca es obligatoria.");
        }
    }
}
