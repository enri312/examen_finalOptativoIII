using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperProjectDapperFinal.Models;
using SuperProjectDapperFinal.Repositories.Interface;
using SuperProjectDapperFinal.Services.Interface;

namespace SuperProjectDapperFinal.Services.Implementacion
{
    public class SucursalService : ISucursalService
    {
        private readonly ISucursalRepository _sucursalRepository;

        public SucursalService(ISucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public async Task<Sucursal> GetByIdAsync(int id)
        {
            return await _sucursalRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Sucursal>> GetAllAsync()
        {
            return await _sucursalRepository.GetAllAsync();
        }

        public async Task<int> AddAsync(Sucursal sucursal)
        {
            ValidateSucursal(sucursal);
            return await _sucursalRepository.AddAsync(sucursal);
        }

        public async Task<bool> UpdateAsync(Sucursal sucursal)
        {
            ValidateSucursal(sucursal);
            return await _sucursalRepository.UpdateAsync(sucursal);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _sucursalRepository.DeleteAsync(id);
        }

        private void ValidateSucursal(Sucursal sucursal)
        {
            if (string.IsNullOrWhiteSpace(sucursal.descripcion) || sucursal.descripcion.Length < 3)
                throw new ArgumentException("La descripción es obligatoria y debe tener al menos 3 caracteres.");
            if (string.IsNullOrWhiteSpace(sucursal.direccion) || sucursal.direccion.Length < 5)
                throw new ArgumentException("La dirección es obligatoria y debe tener al menos 5 caracteres.");
            if (!string.IsNullOrWhiteSpace(sucursal.telefono) && sucursal.telefono.Length < 10)
                throw new ArgumentException("El teléfono debe tener al menos 10 caracteres.");
            if (!string.IsNullOrWhiteSpace(sucursal.whatsapp) && sucursal.whatsapp.Length < 10)
                throw new ArgumentException("El WhatsApp debe tener al menos 10 caracteres.");
            if (!string.IsNullOrWhiteSpace(sucursal.mail) && !IsValidEmail(sucursal.mail))
                throw new ArgumentException("El mail debe ser una dirección válida.");
            if (sucursal.estado == null)
                throw new ArgumentException("El estado es obligatorio.");
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
    }
}
