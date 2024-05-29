using System.Net;
using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementSystem.Controllers


{

    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class IssuesController : Controller
    {

        //Here we 4 entity to connect our CosmosDatabase
        private string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryDB";
        public string ContainerName = "Issues";

        public Container container;


        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }


        private Container GetBookContainer()
        {
            var bookDatabaseName = "LibraryManager";
            var bookContainerName = "Book";
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(bookDatabaseName);
            return database.GetContainer(bookContainerName);
        }

        public IssuesController()
        {
            container = GetContainer();
        }


        //When user issues book from the library Issue book entity should be created
        [HttpPost]
        public async Task<IActionResult> IssueBook(IssuesEntity issue)
        {
            issue.Id = Guid.NewGuid().ToString();
            issue.UId = Guid.NewGuid().ToString();
            issue.IssueDate = DateTime.UtcNow;
            issue.IsReturned = false;
            issue.CreatedOn = DateTime.UtcNow;
            issue.UpdatedOn = DateTime.UtcNow;
            issue.Version = 1;
            issue.Active = true;
            issue.Archived = false;

            var bookContainer = GetBookContainer();
            var bookQuery = bookContainer.GetItemLinqQueryable<Book>(true).Where(b => b.Id == issue.BookId).FirstOrDefault();

            if (bookQuery == null)
            {
                return NotFound("Book not found.");
            }
            bookQuery.IsIssued = true;
            await bookContainer.ReplaceItemAsync(bookQuery, bookQuery.Id);

            await container.CreateItemAsync(issue);

            return CreatedAtAction(nameof(GetIssueByUid), new { id = issue.Id }, issue);
        }



        //User should be able to get issue book by UId
        [HttpGet("{uid}")]
        public async Task<Issue> GetIssueByUid(string uId)
        {
            var getIssue = container.GetItemLinqQueryable<Issue>(true).Where(q => q.UId == uId).FirstOrDefault();
            return getIssue;
        }


        //User should be able to update existing issue entity
        [HttpPost]
        public async Task<Issue> UpdateIssue(Issue issue)
        {
            var existingIssueResponse = await container.ReadItemAsync<Issue>(issue.UId, new PartitionKey(issue.UId));
            if (existingIssueResponse.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException("Issue not found.");
            }

            var existingIssue = existingIssueResponse.Resource;

            existingIssue.BookId = issue.BookId;
            existingIssue.MemberId = issue.MemberId;
            existingIssue.IssueDate = issue.IssueDate;
            existingIssue.ReturnDate = issue.ReturnDate;
            existingIssue.IsReturned = issue.IsReturned;

            var updatedIssueResponse = await container.ReplaceItemAsync(existingIssue, existingIssue.UId, new PartitionKey(existingIssue.UId));

            if (updatedIssueResponse.StatusCode == HttpStatusCode.OK)
            {
                return updatedIssueResponse.Resource;
            }
            else
            {
                throw new Exception("Failed to update the issue.");
            }
        }
    }
}



