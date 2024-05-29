using LibraryManagementSystem.Entites;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;


namespace LibraryManagementSystem.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class IssueController : Controller
    {
        public Container container;
        public IssueController() 
        {
            container = GetContainer();
        }

        string URI = "https://localhost:8081";
        string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        string DatabaseName = "LMS";
        string ContainerName = "IssueContainer";


        [HttpPost]
        public async Task<IssueModel> AddIssueBook(IssueModel issueModel)
        {
              IssueEntites issueEntites = new IssueEntites();
             issueEntites.UId = issueModel.UId;
            issueEntites.BookId = issueModel.BookId;
            issueEntites.MemberId = issueModel.MemberId;    
            issueEntites.IssueDate = issueModel.IssueDate;
            issueEntites.ReturnDate = issueModel.ReturnDate;
            issueEntites.IsReturned = issueModel.isReturned;

            issueEntites.Id = Guid.NewGuid().ToString();
            issueEntites.UId = issueEntites.Id;
            issueEntites.CreatedBy = "mohit";
            issueEntites.CreatedOn = DateTime.Now;
            issueEntites.UpdatedBy = "mohit";
            issueEntites.UpdatedOn = DateTime.Now;
            issueEntites.Version = issueEntites.Version + 1;
            issueEntites.Active = true;
            issueEntites.Archived = false;

            IssueEntites response = await container.CreateItemAsync(issueEntites);

            IssueModel model = new IssueModel();    
            model.UId = response.UId;
            model.BookId = response.BookId;
            model.MemberId = response.MemberId;
            model.IssueDate = response.IssueDate;
            model.ReturnDate = response.ReturnDate;
            model.isReturned = response.IsReturned;

            return model;

        }

        [HttpGet]
        public async Task<IssueModel> GetByIssueUid(string UId)
        {
            var issue = container.GetItemLinqQueryable<IssueEntites>(true).Where(q => q.UId == UId && q.Active == true && q.Archived == false).FirstOrDefault();

            IssueModel issueModel = new IssueModel();
            issueModel.UId = issue.UId;
            issueModel.BookId = issue.BookId;
            issueModel.MemberId = issue.MemberId;
            issueModel.ReturnDate = issue.ReturnDate;
            issueModel.isReturned = issue.IsReturned;   

            return issueModel;
        }

        [HttpPut]
        public async Task<IssueModel> UpdateIssue(IssueModel issueModel)
        {
            var existingissue = container.GetItemLinqQueryable<IssueEntites>(true).Where(q => q.UId == issueModel.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            existingissue.Active = false;
            existingissue.Archived = true;

            await container.ReplaceItemAsync(existingissue, existingissue.Id);

            existingissue.Id = Guid.NewGuid().ToString();
            existingissue.UpdatedBy = "mohit";
            existingissue.UpdatedOn = DateTime.Now;
            existingissue.Version = existingissue.Version + 1;
            existingissue.Active = true;
            existingissue.Archived = false;

            existingissue.BookId = issueModel.BookId;
            existingissue.MemberId = issueModel.MemberId;
            existingissue.IssueDate = issueModel.IssueDate;
            existingissue.ReturnDate = issueModel.ReturnDate;
            existingissue.IsReturned = issueModel.isReturned;

            existingissue = await container.CreateItemAsync(existingissue);

            IssueModel model = new IssueModel();
            model.UId = existingissue.UId;
            model.MemberId = existingissue.MemberId;
            model.BookId = existingissue.BookId;
            model.IssueDate = existingissue.IssueDate;
            model.ReturnDate = existingissue.ReturnDate;
            model.isReturned = existingissue.IsReturned;

            return model;

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
