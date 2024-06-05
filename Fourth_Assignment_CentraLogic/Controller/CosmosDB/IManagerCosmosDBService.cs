using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.CosmosDB
{
    public interface IManagerCosmosDBService
    {
        Task<IEnumerable<Manager>> GetManagersAsync(string query);
        Task<Manager> GetManagerAsync(string id);
        Task AddManagerAsync(Manager manager);
        Task UpdateManagerAsync(string id, Manager manager);
        Task DeleteManagerAsync(string id);
    }
}
