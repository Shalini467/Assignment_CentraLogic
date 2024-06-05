using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface IVisitorService
    {
        Task<IEnumerable<Visitor>> GetAllVisitorsAsync();
        Task<Visitor> GetVisitorByIdAsync(string id);
        Task<Visitor> CreateVisitorAsync(Visitor visitor);
        Task<Visitor> UpdateVisitorAsync(string id, Visitor visitor);
        Task DeleteVisitorAsync(string id);
    }
}
