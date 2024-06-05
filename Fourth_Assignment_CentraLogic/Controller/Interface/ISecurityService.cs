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
        
    }
}
