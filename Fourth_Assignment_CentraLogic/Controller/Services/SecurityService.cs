using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.CosmosDB;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Services
{

    public class SecurityService : ISecurityService
    {
        private readonly IOfficeCosmosDBService _securityCosmosDBService;

        public SecurityService(IOfficeCosmosDBService securityCosmosDBService)
        {
            _securityCosmosDBService = securityCosmosDBService;
        }

        public async Task<IEnumerable<Security>> GetAllSecuritiesAsync()
        {
            return await _securityCosmosDBService.GetSecuritiesAsync("SELECT * FROM c");
        }

        public async Task<Security> GetSecurityByIdAsync(string id)
        {
            return await _securityCosmosDBService.GetSecurityAsync(id);
        }

        public async Task<Security> CreateSecurityAsync(Security security)
        {
            security.Id = Guid.NewGuid().ToString();
            await _securityCosmosDBService.AddSecurityAsync(security);
            return security;
        }

        public async Task<Security> UpdateSecurityAsync(string id, Security security)
        {
            await _securityCosmosDBService.UpdateSecurityAsync(id, security);
            return security;
        }

        public async Task DeleteSecurityAsync(string id)
        {
            await _securityCosmosDBService.DeleteSecurityAsync(id);
        }

        public Task UpdateManagerAsync(string id, Manager manager)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManagerAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task CreateManagerAsync(Manager manager)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Manager>> GetManagerByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Manager>> GetAllManagersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
