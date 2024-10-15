using personapi_dotnet.Models.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace personapi_dotnet.Models.Repositories
{
    public interface IEstudioRepository
    {
        Task<List<Estudio>> GetAllEstudiosAsync();
        Task<Estudio> GetEstudioByIdAsync(int? idProf, int? ccPer);
        Task AddEstudioAsync(Estudio estudio);
        Task UpdateEstudioAsync(Estudio estudio);
        Task DeleteEstudioAsync(int idProf, int ccPer);
        Task<bool> EstudioExistsAsync(int idProf, int ccPer);
    }
}
