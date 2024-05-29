using Newtonsoft.Json;

namespace LibraryManagementSystem.Entites
{
    public class IssueEntites
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "uId")]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "bookId")]
        public string BookId { get; set; }

        [JsonProperty(PropertyName = "memberId")]
        public string MemberId { get; set; }

        [JsonProperty(PropertyName = "issueDate")]
        public DateTime IssueDate { get; set; }

        [JsonProperty(PropertyName = "returnDate")]
        public DateTime? ReturnDate { get; set; }

        [JsonProperty(PropertyName = "isReturned")]
        public bool IsReturned { get; set; }
        [JsonProperty(PropertyName = "documentType")]
        public string DocumentType { get; set; } = "issue";

        [JsonProperty(PropertyName = "createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "updatedBy")]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "updatedOn")]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "version")]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; } = true;

        [JsonProperty(PropertyName = "archived")]
        public bool Archived { get; set; } = false;
    }
}
