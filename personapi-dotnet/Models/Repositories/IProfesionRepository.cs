using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Repositories
{
    public interface IProfesionRepository
    {
        Task<List<Profesion>> GetAllProfesionsAsync();
        Task<Profesion> GetProfesionByIdAsync(int? id);
        Task AddProfesionAsync(Profesion profesion);
        Task UpdateProfesionAsync(Profesion profesion);
        Task DeleteProfesionAsync(int id);
        Task<bool> ProfesionExistsAsync(int id);
    }
}
