
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.CosmosDB
{
    public interface IVisitorCosmosDBService
    {
        Task<IEnumerable<Visitor>> GetVisitorsAsync(string query);
        Task<Visitor> GetVisitorAsync(string id);
        Task AddVisitorAsync(Visitor visitor);
        Task UpdateVisitorAsync(string id, Visitor visitor);
        Task DeleteVisitorAsync(string id);

    }
}
