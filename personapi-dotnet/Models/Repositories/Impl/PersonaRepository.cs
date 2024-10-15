using personapi_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace personapi_dotnet.Models.Repositories.Impl
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonaDbContext _context;

        public PersonaRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Persona>> GetAllPersonasAsync()
        {
            return await _context.Personas.ToListAsync();
        }

        public async Task<Persona> GetPersonaByIdAsync(int? id)
        {
            return await _context.Personas.FirstOrDefaultAsync(m => m.Cc == id);
        }

        public async Task AddPersonaAsync(Persona persona)
        {
            _context.Add(persona);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonaAsync(Persona persona)
        {
            _context.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonaAsync(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> PersonaExistsAsync(int id)
        {
            return await _context.Personas.AnyAsync(e => e.Cc == id);
        }
    }
}
