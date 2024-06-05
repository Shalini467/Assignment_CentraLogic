using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface ISecurityService
    {

        Task<IEnumerable<Security>> GetAllSecuritiesAsync();
        Task<Security> GetSecurityByIdAsync(string id);
        Task<Security> CreateSecurityAsync(Security security);
        Task<Security> UpdateSecurityAsync(string id, Security security);
        Task DeleteSecurityAsync(string id);
        Task UpdateManagerAsync(string id, Manager manager);
        Task DeleteManagerAsync(string id);
        Task CreateManagerAsync(Manager manager);
        Task<ActionResult<Manager>> GetManagerByIdAsync(string id);
        Task<IEnumerable<Manager>> GetAllManagersAsync();
    }
}
