using VisitorSecurityClearanceSystem.CosmosDB;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeCosmosDBService _officeCosmosDBService;

        public OfficeService(IOfficeCosmosDBService officeCosmosDBService)
        {
            _officeCosmosDBService = officeCosmosDBService;
        }


        public async Task<IEnumerable<Office>> GetAllOfficesAsync()
        {
            return await _officeCosmosDBService.GetOfficesAsync("SELECT * FROM c");
        }

        public async Task<Office> GetOfficeByIdAsync(string id)
        {
            return await _officeCosmosDBService.GetOfficeAsync(id);
        }

        public async Task<Office> CreateOfficeAsync(Office office)
        {
            office.Id = Guid.NewGuid().ToString();
            await _officeCosmosDBService.AddOfficeAsync(office);
            return office;
        }

        public async Task<Office> UpdateOfficeAsync(string id, Office office)
        {
            await _officeCosmosDBService.UpdateOfficeAsync(id, office);
            return office;
        }

        public async Task DeleteOfficeAsync(string id)
        {
            await _officeCosmosDBService.DeleteOfficeAsync(id);
        }


    }
}
