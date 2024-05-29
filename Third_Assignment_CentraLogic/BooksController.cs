
using System;
using System.Net;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Http;


namespace LibraryManagementSystem.Controllers
{


    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class BooksController: Controller
    {
        //Here we use 4 entity to connect our CosmosDatabase

        private string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryDB";
        public string ContainerName = "Books";

        public Container container;

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }

        public BooksController()
        {
            container = GetContainer();
        }

        //Add book to the library

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            book.UId = Guid.NewGuid().ToString();
            book.Id = Guid.NewGuid().ToString();

            var newBook = await container.CreateItemAsync(book);
            return Ok(newBook);
        }

        //Retrieve a particular book by its UId

        [HttpGet]
        public async Task<Book> GetBookByUid(string uId)
        {
            var getBook= container.GetItemLinqQueryable<Book>(true).Where(q => q.UId == uId).FirstOrDefault();
            return getBook;
        }


        //Retrieve book by its name

        [HttpGet("{title}")]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            var book = container.GetItemLinqQueryable<Book>(true)
                .Where(b => b.Title == title)
                .AsEnumerable()
                .FirstOrDefault();

            if (book == null)
            {
                return NotFound("No book found with the given title.");
            }

            return Ok(book);
        }



        //Retrieve all books

        [HttpGet]
        public async Task<List<Book>> GetAllBooks()
        {
            var allBooks = container.GetItemLinqQueryable<Book>(true).ToList();
            return allBooks;
        }



        //Retrieve all Available books which are not issued
        [HttpGet]
        public async Task<List<Book>> GetAllUnissuedBook()
        {
            var unIssuedBook = container.GetItemLinqQueryable<Book>(true).Where(q => q.IsIssued == false).ToList();
            return unIssuedBook;
        }


        //Retrieve All Issued Books
        [HttpGet]
        public async Task<List<Book>> GetAllIssuedBooks()
        {
            var issuedBook = container.GetItemLinqQueryable<Book>(true).Where(q => q.IsIssued == true).ToList();
            return issuedBook;
        }



        //Update book
        [HttpPost]
        public async Task<Book> UpdateBook(Book book)
        {
            // Retrieve the existing book from Cosmos DB
            var existingBookResponse = await container.ReadItemAsync<Book>(book.UId, new PartitionKey(book.UId));
            if (existingBookResponse.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException("Book not found.");
            }

            var existingBook = existingBookResponse.Resource;

            // Update the properties of the existing book
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.ISBN = book.ISBN;
            existingBook.IsIssued = book.IsIssued;

            // Replace the existing book in Cosmos DB
            var updatedBookResponse = await container.ReplaceItemAsync(existingBook, existingBook.UId, new PartitionKey(existingBook.UId));

            // If the update was successful, return the updated book
            if (updatedBookResponse.StatusCode == HttpStatusCode.OK)
            {
                return updatedBookResponse.Resource;
            }
            else
            {
                throw new Exception("Failed to update the book.");
            }
        }



    }


    }

