using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface IOfficeService
    {
        Task<IEnumerable<Office>> GetAllOfficesAsync();
        Task<Office> GetOfficeByIdAsync(string id);
        Task<Office> CreateOfficeAsync(Office office);
        Task<Office> UpdateOfficeAsync(string id, Office office);
        Task DeleteOfficeAsync(string id);
    }
}
