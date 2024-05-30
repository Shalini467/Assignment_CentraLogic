
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

        
        // Here I am adding the Book Entity 

        [HttpPost]
        public async Task<Book> AddBookEntity(Book book)
        {
            BooksEntity entity = new BooksEntity();
            entity.Title = book.Title;
            entity.Author = book.Author;
            entity.PublishedDate = book.PublishedDate;
            entity.ISBN = book.ISBN;
            entity.IsIssued = book.IsIssued;

            entity.Id = Guid.NewGuid().ToString();
            entity.UId = entity.Id;
            entity.DocumentType = "Book";
            entity.CreatedBy = "Shalini";
            entity.CreatedOn = DateTime.Now;
            entity.UpdatedBy = "Shalini";
            entity.UpdatedOn = DateTime.Now;
            entity.Version = 1;
            entity.Active = true;
            entity.Archived = false;

            BooksEntity response = await container.CreateItemAsync(entity);

            Book responseBook = new Book();
            responseBook.Title = response.Title;
            responseBook.Author = response.Author;
            responseBook.PublishedDate = response.PublishedDate;
            responseBook.ISBN = response.ISBN;
            responseBook.IsIssued = response.IsIssued;
            return responseBook;
        }




        //Update the Book

        [HttpPost]

        public async Task<Book> UpdateBook(Book book)
        {

            var existingBook = container.GetItemLinqQueryable<BooksEntity>(true).Where(q => q.UId == book.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            if (existingBook == null)
            {
                throw new InvalidOperationException("Book is not found or it is already archived.");
            }
            existingBook.Archived = true;
            existingBook.Active = false;
            await container.ReplaceItemAsync(existingBook, existingBook.Id);


            existingBook.Id = Guid.NewGuid().ToString();
            existingBook.UpdatedBy = "Shalini";
            existingBook.UpdatedOn = DateTime.Now;
            existingBook.Version = existingBook.Version + 1;
            existingBook.Active = true;
            existingBook.Archived = false;



            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.ISBN = book.ISBN;
            existingBook.IsIssued = book.IsIssued;
            existingBook = await container.CreateItemAsync(existingBook);


            Book responce = new Book();
            responce.UId = existingBook.UId;
            responce.Title = existingBook.Title;
            responce.Author = existingBook.Author;
            responce.PublishedDate = existingBook.PublishedDate;
            responce.ISBN = existingBook.ISBN;
            responce.IsIssued = existingBook.IsIssued;
            return responce;


        }



    }


    }

