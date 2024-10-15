using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Repositories
{
    public interface IPersonaRepository
    {
        Task<List<Persona>> GetAllPersonasAsync();
        Task<Persona> GetPersonaByIdAsync(int? id);
        Task AddPersonaAsync(Persona persona);
        Task UpdatePersonaAsync(Persona persona);
        Task DeletePersonaAsync(int id);
        Task<bool> PersonaExistsAsync(int id);  
    }
}
