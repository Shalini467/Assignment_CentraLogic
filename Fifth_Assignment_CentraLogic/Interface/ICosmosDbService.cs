namespace EmployeeManagementSystemDI.Interface
{
    public interface ICosmosDbService<T> where T : class
    {

        Task<IEnumerable<T>> GetItemsAsync(string query);
        Task<T> GetItemAsync(string id);
        Task AddItemAsync(T item);
        Task UpdateItemAsync(string id, T item);
        Task DeleteItemAsync(string id);
    }
}
