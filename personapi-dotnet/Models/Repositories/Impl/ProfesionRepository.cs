using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProfesionRepository : IProfesionRepository
{
    private readonly PersonaDbContext _context;

    public ProfesionRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Profesion>> GetAllProfesionsAsync()
    {
        return await _context.Profesions.ToListAsync();
    }

    public async Task<Profesion> GetProfesionByIdAsync(int? id)
    {
        return await _context.Profesions.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddProfesionAsync(Profesion profesion)
    {
        _context.Add(profesion);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProfesionAsync(Profesion profesion)
    {
        _context.Update(profesion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProfesionAsync(int id)
    {
        var profesion = await _context.Profesions.FindAsync(id);
        if (profesion != null)
        {
            _context.Profesions.Remove(profesion);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ProfesionExistsAsync(int id)
    {
        return await _context.Profesions.AnyAsync(e => e.Id == id);
    }
}
