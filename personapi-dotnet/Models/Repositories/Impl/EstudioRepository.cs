using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace personapi_dotnet.Models.Repositories.Impl
{
    public class EstudioRepository : IEstudioRepository
    {
        private readonly PersonaDbContext _context;

        public EstudioRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Estudio>> GetAllEstudiosAsync()
        {
            return await _context.Estudios.Include(e => e.CcPerNavigation).Include(e => e.IdProfNavigation).ToListAsync();
        }

        public async Task<Estudio> GetEstudioByIdAsync(int? id)
        {
            return await _context.Estudios
                                 .Include(e => e.CcPerNavigation)
                                 .Include(e => e.IdProfNavigation)
                                 .FirstOrDefaultAsync(e => e.IdProf == id);
        }

        public async Task AddEstudioAsync(Estudio estudio)
        {
            _context.Estudios.Add(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEstudioAsync(Estudio estudio)
        {
            _context.Estudios.Update(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEstudioAsync(int id)
        {
            var estudio = await _context.Estudios.FindAsync(id);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EstudioExistsAsync(int id)
        {
            return await _context.Estudios.AnyAsync(e => e.IdProf == id);
        }
    }
}
