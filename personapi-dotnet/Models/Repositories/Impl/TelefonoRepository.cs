using personapi_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace personapi_dotnet.Models.Repositories.Impl
{
    public class TelefonoRepository : ITelefonoRepository
    {
        private readonly PersonaDbContext _context;

        public TelefonoRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Telefono>> GetAllTelefonosAsync()
        {
            return await _context.Telefonos.Include(t => t.DuenioNavigation).ToListAsync();
        }

        public async Task<Telefono> GetTelefonoByIdAsync(string id)
        {
            return await _context.Telefonos.Include(t => t.DuenioNavigation)
                                           .FirstOrDefaultAsync(m => m.Num == id);
        }

        public async Task AddTelefonoAsync(Telefono telefono)
        {
            _context.Add(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTelefonoAsync(Telefono telefono)
        {
            _context.Update(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTelefonoAsync(string id)
        {
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TelefonoExistsAsync(string id)
        {
            return await _context.Telefonos.AnyAsync(e => e.Num == id);
        }

        public async Task<bool> HasTelefonosAsync(int personaId)
        {
            return await _context.Telefonos.AnyAsync(t => t.Duenio == personaId);
        }

    }
}
