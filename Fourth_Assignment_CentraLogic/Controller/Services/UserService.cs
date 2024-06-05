using Microsoft.Azure.Cosmos;
using VisitorSecurityClearanceSystem.Controllers;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Services
{
    public class UserService : IUserInterface
    {

        
        
           public readonly Container _container;
        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("URI");
            string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
            string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
            string ContainerName = Environment.GetEnvironmentVariable("ContainerName");
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);

            Database database = cosmosclient.GetDatabase(DatabaseName);

            Container container = database.GetContainer(ContainerName);

            return container;
        }
        public UserService()
        {
            _container = GetContainer();

        }
            public async Task<User> UserRegister(User user)
            {
                user.Id = Guid.NewGuid().ToString();
                user.Username = "Shalini";
                user.DocumnetType = "dtype";
                user.Version = 1;
                user.CreatedBy = "Shalini";
                user.CreatedOn = DateTime.Now;
                user.UpdatedBy = "Shalini";
                user.UpdatedOn = DateTime.Now;
                user.Active = true;
                user.Archived = false;

                try
                {
                    ItemResponse<User> response = await _container.CreateItemAsync(user);
                    return response.Resource;
                }
                catch (CosmosException ex)
                {
                    // Handle exception (log it, rethrow it, etc.)
                    throw new Exception("Error creating user item in Cosmos DB", ex);
                }
            }

            public async Task<string> LoginUser(User user)
            {
                var query = _container.GetItemLinqQueryable<User>()
                                      .Where(u => u.Username == user.Userame && u.Password == user.Password)
                                      .Take(1)
                                      .AsQueryable();

                var iterator = query.ToFeedIterator();
                if (iterator.HasMoreResults)
                {
                    foreach (var item in await iterator.ReadNextAsync())
                    {
                        return item.Username;
                    }
                }

                return null;
            }
        }
}
