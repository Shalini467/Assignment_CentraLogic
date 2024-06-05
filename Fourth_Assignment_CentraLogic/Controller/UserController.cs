using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
public readonly Container _container;
        public IUserInterface _userInterface;

        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("URI");
            string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
            string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
            string ContainerName = Environment.GetEnvironmentVariable("ContainerName");
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);

            Database databse = cosmosclient.GetDatabase(DatabaseName);

            Container container = databse.GetContainer(ContainerName);

            return container;
        }

        public UserController(IUserInterface userInterface)
        {
            _container = GetContainer();
            _userInterface = userInterface;

        }


        [HttpPost]
        public async Task<IActionResult> UserRegister(UserDTO userDTO)
        {
            User user = new Use
            {
                Id = userDTO.Id,
                Username = userDTO.Username,
                Password = userDTO.Password,
                PhoneNumber = userDTO.PhoneNumber,
                Email = userDTO.Email
            };

            var response = await _userInterface.UserRegister(user);

            userDTO.Username = response.UserName;
            userDTO.Id = response.Id;
            userDTO.PhoneNumber = response.PhoneNumber;
            userDTO.Password = response.Password;
            userDTO.Email = response.Email;

            return Ok(userDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(User user)
        {
            var result = await _userInterface.LoginUser(user);

            if (result != null)
            {
                return Ok(new
                {
                    Message = "Login successful",
                    UId = result
                });
            }

            return Unauthorized(new { Message = "Invalid credentials." });
        }
    }

    public class CosmosDbService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private readonly string _containerName;

        public CosmosDbService(IOptions<CosmosDbSettings> settings)
        {
            var config = settings.Value;
            _cosmosClient = new CosmosClient(config.URI, config.PrimaryKey);
            _databaseName = config.DatabaseName;
            _containerName = config.ContainerName;
        }

        public Container GetContainer()
        {
            Database database = _cosmosClient.GetDatabase(_databaseName);
            return database.GetContainer(_containerName);
        }
    }
}
