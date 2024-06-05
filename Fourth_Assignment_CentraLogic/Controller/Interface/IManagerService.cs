using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface IManagerService
    {

        Task<IEnumerable<Manager>> GetAllManagersAsync();
        Task<Manager> GetManagerByIdAsync(string id);
        Task<Manager> CreateManagerAsync(Manager manager);
        Task<Manager> UpdateManagerAsync(string id, Manager manager);
        Task DeleteManagerAsync(string id);
    }
}
