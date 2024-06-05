
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.CosmosDB
{
    public interface IOfficeCosmosDBService
    {
        Task<IEnumerable<Office>> GetOfficesAsync(string query);
        Task<Office> GetOfficeAsync(string id);
        Task AddOfficeAsync(Office office);
        Task UpdateOfficeAsync(string id, Office office);
        Task DeleteOfficeAsync(string id);
        
    }
}
