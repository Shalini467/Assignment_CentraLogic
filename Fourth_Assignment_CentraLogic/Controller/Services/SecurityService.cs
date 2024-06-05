using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.CosmosDB;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Services
{

    public class SecurityService : ISecurityService
    {
        public SecurityService(ISecurityCosmosDBService securityCosmosDBService)
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
    }
}
