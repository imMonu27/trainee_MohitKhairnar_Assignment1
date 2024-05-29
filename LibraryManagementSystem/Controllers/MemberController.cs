using LibraryManagementSystem.Entites;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        public Container container;

        string URI = "https://localhost:8081";
        string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        string DatabaseName = "LMS";
        string ContainerName = "memberContainer";

        public MemberController()
        {
            container = GetContainer();
        }

        [HttpPost]
        public async Task<MemberModel> AddMember(MemberModel memberModel)
        {
            MemberEntites memberEntites = new MemberEntites();
            memberEntites.Name = memberModel.Name;
            memberEntites.Email = memberModel.Email;

            //assign important 
            memberEntites.Id = Guid.NewGuid().ToString();
            memberEntites.UId = memberEntites.Id;
            memberEntites.CreatedBy = "mohit";
            memberEntites.CreatedOn = DateTime.Now;
            memberEntites.UpdatedBy = "mohit";
            memberEntites.UpdatedOn = DateTime.Now;
            memberEntites.Version = memberEntites.Version + 1;
            memberEntites.Active = true;
            memberEntites.Archived = false;

            //add data to database
            MemberEntites response = await container.CreateItemAsync(memberEntites);

            //return the model 
            MemberModel responseModel = new MemberModel();
            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.DateOfBirth = responseModel.DateOfBirth;
            responseModel.Email = response.Email;
            return responseModel;


        }
        

        [HttpGet]
        public async Task<MemberModel> GetUserById(string UId)
        {
            //1. get record
            var member = container.GetItemLinqQueryable< MemberEntites>(true).Where(x => x.UId == UId && x.Active==true && x.Archived == false ).FirstOrDefault();

            //2. map the fields
            MemberModel memberModel = new MemberModel();
            memberModel.UId = member.UId;
            memberModel.Name = member.Name; 
            memberModel.DateOfBirth= member.DateOfBirth;
            memberModel.Email = member.Email;

            //3. return
            return memberModel;
        }

        [HttpGet]
        public async Task<List<MemberModel>> GetAllMembers()
        { 
            //1. fetch the records 
            var members = container.GetItemLinqQueryable<MemberEntites>(true).Where(q=>q.Active == true && q.Archived == false && q.DocumentType == "member").ToList();

            //2. map the fields to model
            List<MemberModel> memberModels = new List<MemberModel>();

            foreach (var member in members)
            {
                MemberModel Model = new MemberModel();
                Model.UId = member.UId;
                Model.Name = member.Name;
                Model.DateOfBirth = member.DateOfBirth;
                Model.Email = member.Email;

                memberModels.Add(Model);
            }

            //3. Return
            return memberModels;
        }

        [HttpPut]
        public async Task<MemberModel> UpdateMember(MemberModel member)
        {
            //1. get the existing record by UId
            var existingMember = container.GetItemLinqQueryable<MemberEntites>(true).Where(x => x.UId == member.UId && x.Active == true && x.Archived == false && x.DocumentType == "member").FirstOrDefault();
            
            //2. Replace the records
            existingMember.Active = false;
            existingMember.Archived = true;

            await container.ReplaceItemAsync(existingMember, existingMember.Id);

            //3. Assign the values for mandatory fields
            existingMember.Id = Guid.NewGuid().ToString();
            existingMember.UpdatedBy = "admin";
            existingMember.UpdatedOn = DateTime.Now;
            existingMember.Version = existingMember.Version + 1;
            existingMember.Active = true;
            existingMember.Archived = false;

            //4. Assign the values to the fields which we will get from request obj
            existingMember.UId = member.UId;
            existingMember.Name = member.Name;
            existingMember.DateOfBirth= member.DateOfBirth;
            existingMember.Email = member.Email;

            //5. Add the data to database
            existingMember = await container.CreateItemAsync(existingMember);

            //6.return 
            MemberModel response = new MemberModel();
            response.UId = member.UId;
            response.Name = member.Name;
            response.DateOfBirth = member.DateOfBirth;
            response.Email = member.Email;

            return response;

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
