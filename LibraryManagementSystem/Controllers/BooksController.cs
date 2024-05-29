using LibraryManagementSystem.Entites;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;


namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : Controller
    {


        public Container container;

        string URI = "https://localhost:8081";
        string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        string DatabaseName = "LMS";
        string ContainerName = "Bookcontainer";

        public BooksController()
        {
            container = GetContainer();
        }

        [HttpPost]
        public async Task<BookModel> addBook(BookModel book)
        {
            BookEntites bookEntites = new BookEntites();
            bookEntites.UId = book.UId;
            bookEntites.Author = book.Author;
            bookEntites.Title = book.Title;
            bookEntites.PublishedDate = book.PublishedDate;
            bookEntites.ISBN = book.ISBN;
            bookEntites.IsIssued = book.IsIssued;

            bookEntites.Id = Guid.NewGuid().ToString();
            bookEntites.UId = bookEntites.Id;
            bookEntites.CreatedBy = "mohit";
            bookEntites.CreatedOn = DateTime.Now;
            bookEntites.UpdatedBy = "mohit";
            bookEntites.UpdatedOn = DateTime.Now;
            bookEntites.Version = bookEntites.Version + 1;
            bookEntites.Active = true;
            bookEntites.Archived = false;

            BookEntites response = await container.CreateItemAsync(bookEntites);
            
            BookModel responseModel = new BookModel();
            responseModel.UId = response.UId;
            responseModel.Title = response.Title;   
            responseModel.PublishedDate = response.PublishedDate;
            responseModel.Author = response.Author;
            responseModel.IsIssued = response.IsIssued;
            responseModel.ISBN = response.ISBN;

            return responseModel;
        }

        [HttpGet]
        public async Task<BookModel> GetBookByUid(string uid)
        {
            var book = container.GetItemLinqQueryable<BookEntites>(true).Where(q=>q.UId == uid && q.Active == true && q.Archived == false).FirstOrDefault();

            BookModel bookModel = new BookModel();
            bookModel.UId = book.UId;
            bookModel.Author = book.Author;
            bookModel.Title = book.Title;
            bookModel.PublishedDate = book.PublishedDate;
            bookModel.ISBN = book.ISBN;
            bookModel.IsIssued = book.IsIssued;

            return bookModel;
        }

        [HttpGet]
        public async Task<BookModel> GetBookByName(string Title)
        {
            var books = container.GetItemLinqQueryable<BookEntites>(true).Where(q => q.Title == Title && q.Active == true && q.Archived == false).FirstOrDefault();
            BookModel bookModel = new BookModel();
            bookModel.UId = books.UId;
            bookModel.Author = books.Author;
            bookModel.Title = books.Title;
            bookModel.PublishedDate= books.PublishedDate;
            bookModel.ISBN= books.ISBN;
            bookModel.IsIssued= books.IsIssued; 

            return bookModel;
        }

        [HttpGet]
        public async Task<List<BookModel>> GetAllBooks()
        {
            var book = container.GetItemLinqQueryable<BookEntites>(true).Where(q=>q.Active == true && q.Archived== false && q.DocumentType == "book").ToList();

            List<BookModel> bookModels = new List<BookModel>();

            foreach (var item in book)
            {
                BookModel bookModel = new BookModel();
                bookModel.UId = item.UId;
                bookModel.Author = item.Author;
                bookModel.Title = item.Title;
                bookModel.PublishedDate= item.PublishedDate;
                bookModel.ISBN = item.ISBN;
                bookModel.IsIssued = item.IsIssued;

                bookModels.Add(bookModel);
            }

            return bookModels;
        }

        [HttpGet]
        public async Task<List<BookModel>> GetAllNotIssuedBook()
        {
            var books = container.GetItemLinqQueryable<BookEntites>(true).Where(q=>q.IsIssued == false && q.Active == true && q.Archived == false ).ToList();

            List<BookModel> bookmodel = new List<BookModel>();

            foreach(var item in books)
            {
                BookModel Model=new BookModel();
                Model.UId = item.UId;
                Model.Author = item.Author;
                Model.Title = item.Title;
                Model.PublishedDate = item.PublishedDate;
                Model.ISBN = item.ISBN;
                Model.IsIssued= item.IsIssued;

                bookmodel.Add(Model);
            }
            return bookmodel;
        }

        [HttpGet]
        public async Task<List<BookModel>> GetAllIssuedBook()
        {
            var books = container.GetItemLinqQueryable<BookEntites>(true).Where(q => q.IsIssued == true && q.Active == true && q.Archived == false).ToList();

            List<BookModel> bookmodel = new List<BookModel>();

            foreach (var item in books)
            {
                BookModel Model = new BookModel();
                Model.UId = item.UId;
                Model.Author = item.Author;
                Model.Title = item.Title;
                Model.PublishedDate = item.PublishedDate;
                Model.ISBN = item.ISBN;
                Model.IsIssued = item.IsIssued;

                bookmodel.Add(Model);
            }
            return bookmodel;
        }

        [HttpPut]
        public async Task<BookModel> UpdateBook(BookModel bookmodel)
        {
            var ExistingBook = container.GetItemLinqQueryable<BookEntites>(true).Where(q=>q.UId ==  bookmodel.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            ExistingBook.Active = false;
            ExistingBook.Archived = true;

            await container.ReplaceItemAsync(ExistingBook , ExistingBook.Id);

            ExistingBook.Id = Guid.NewGuid().ToString();
            ExistingBook.UpdatedBy = "Mohit";
            ExistingBook.UpdatedOn = DateTime.Now;
            ExistingBook.Version = ExistingBook.Version + 1;
            ExistingBook.Active = true;
            ExistingBook.Archived = false;

            ExistingBook.Title = bookmodel.Title;
            ExistingBook.Author = bookmodel.Author;
            ExistingBook.PublishedDate = bookmodel.PublishedDate;
            ExistingBook.ISBN = bookmodel.ISBN;
            ExistingBook.IsIssued = bookmodel.IsIssued;

            await container.CreateItemAsync(ExistingBook);

            BookModel responseModel = new BookModel();
            responseModel.UId = bookmodel.UId;
            responseModel.Title = bookmodel.Title;
            responseModel.Author = bookmodel.Author;
            responseModel.PublishedDate = bookmodel.PublishedDate;
            responseModel.ISBN = bookmodel.ISBN;
            responseModel.IsIssued = bookmodel.IsIssued;

            
            return responseModel;
        }
        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }


    }
}
