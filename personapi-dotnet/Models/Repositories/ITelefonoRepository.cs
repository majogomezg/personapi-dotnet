using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Repositories
{
    public interface ITelefonoRepository
    {
        Task<List<Telefono>> GetAllTelefonosAsync();
        Task<Telefono> GetTelefonoByIdAsync(string id);
        Task AddTelefonoAsync(Telefono telefono);
        Task UpdateTelefonoAsync(Telefono telefono);
        Task DeleteTelefonoAsync(string id);
        Task<bool> TelefonoExistsAsync(string id);
    }
}
