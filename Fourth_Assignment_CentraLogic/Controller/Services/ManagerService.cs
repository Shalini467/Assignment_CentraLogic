
using VisitorSecurityClearanceSystem.CosmosDB;

using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Services
{
    public class ManagerService : IManagerService
    {

        public readonly IManagerCosmosDBService _managerCosmosDBService;

        public ManagerService(IManagerCosmosDBService managerCosmosDBService)
        {
            _managerCosmosDBService = managerCosmosDBService;
        }


        public async Task<IEnumerable<Manager>> GetAllManagersAsync()
        {
            return await _managerCosmosDBService.GetManagersAsync("SELECT * FROM c");
        }

        public async Task<Manager> GetManagerByIdAsync(string id)
        {
            return await _managerCosmosDBService.GetManagerAsync(id);
        }

        public async Task<Manager> CreateManagerAsync(Manager manager)
        {
            manager.Id = Guid.NewGuid().ToString();
            await _managerCosmosDBService.AddManagerAsync(manager);
            return manager;
        }

        public async Task<Manager> UpdateManagerAsync(string id, Manager manager)
        {
            await _managerCosmosDBService.UpdateManagerAsync(id, manager);
            return manager;
        }

        public async Task DeleteManagerAsync(string id)
        {
            await _managerCosmosDBService.DeleteManagerAsync(id);
        }

    }
}
