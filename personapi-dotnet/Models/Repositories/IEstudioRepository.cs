using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Repositories
{
    public interface IEstudioRepository
    {
        Task<List<Estudio>> GetAllEstudiosAsync();
        Task<Estudio> GetEstudioByIdAsync(int? id);
        Task AddEstudioAsync(Estudio estudio);
        Task UpdateEstudioAsync(Estudio estudio);
        Task DeleteEstudioAsync(int id);
        Task<bool> EstudioExistsAsync(int id);
    }
}
