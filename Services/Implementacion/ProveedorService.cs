using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperProjectDapperFinal.Models;
using SuperProjectDapperFinal.Repositories;

namespace SuperProjectDapperFinal.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<Proveedor> GetByIdAsync(int id)
        {
            return await _proveedorRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Proveedor>> GetAllAsync()
        {
            return await _proveedorRepository.GetAllAsync();
        }

        public async Task<int> AddAsync(Proveedor proveedor)
        {
            ValidateProveedor(proveedor);
            return await _proveedorRepository.AddAsync(proveedor);
        }

        public async Task<bool> UpdateAsync(Proveedor proveedor)
        {
            ValidateProveedor(proveedor);
            return await _proveedorRepository.UpdateAsync(proveedor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _proveedorRepository.DeleteAsync(id);
        }

        private void ValidateProveedor(Proveedor proveedor)
        {
            if (string.IsNullOrWhiteSpace(proveedor.razon_social) || proveedor.razon_social.Length < 3)
                throw new ArgumentException("La razón social es obligatoria y debe tener al menos 3 caracteres.");
            if (string.IsNullOrWhiteSpace(proveedor.tipo_documento) || proveedor.tipo_documento.Length < 3)
                throw new ArgumentException("El tipo de documento es obligatorio y debe tener al menos 3 caracteres.");
            if (string.IsNullOrWhiteSpace(proveedor.numero_documento) || proveedor.numero_documento.Length < 3)
                throw new ArgumentException("El número de documento es obligatorio y debe tener al menos 3 caracteres.");
            if (string.IsNullOrWhiteSpace(proveedor.direccion) || proveedor.direccion.Length < 5)
                throw new ArgumentException("La dirección es obligatoria y debe tener al menos 5 caracteres.");
            if (string.IsNullOrWhiteSpace(proveedor.mail) || !proveedor.mail.Contains("@"))
                throw new ArgumentException("El mail es obligatorio y debe ser una dirección válida.");
            if (!string.IsNullOrWhiteSpace(proveedor.celular) && proveedor.celular.Length != 10)
                throw new ArgumentException("El celular debe tener 10 caracteres.");
            if (proveedor.estado == null)
                throw new ArgumentException("El estado es obligatorio.");
        }
    }
}

