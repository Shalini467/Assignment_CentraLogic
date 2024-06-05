

using VisitorSecurityClearanceSystem.Common;
using VisitorSecurityClearanceSystem.CosmosDB;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Model;


namespace VisitorSecurityClearanceSystem.Services
{
    public class VisitorService : IVisitorService
    {

        private readonly IVisitorCosmosDBService _visitorCosmosDBService;

        public VisitorService(IVisitorCosmosDBService visitorCosmosDBService)
        {
            _visitorCosmosDBService = visitorCosmosDBService;
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorsAsync()
        {
            return await _visitorCosmosDBService.GetVisitorsAsync("SELECT * FROM c");
        }

        public async Task<Visitor> GetVisitorByIdAsync(string id)
        {
            return await _visitorCosmosDBService.GetVisitorAsync(id);
        }

        public async Task<Visitor> CreateVisitorAsync(Visitor visitor)
        {
            visitor.Id = Guid.NewGuid().ToString();
            await _visitorCosmosDBService.AddVisitorAsync(visitor);
            return visitor;
        }

        public async Task<Visitor> UpdateVisitorAsync(string id, Visitor visitor)
        {
            await _visitorCosmosDBService.UpdateVisitorAsync(id, visitor);
            return visitor;
        }

        public async Task DeleteVisitorAsync(string id)
        {
            await _visitorCosmosDBService.DeleteVisitorAsync(id);
        }



    }
}
