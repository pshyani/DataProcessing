using System.Text.Json.Serialization;

namespace Emerson.DataProcessing.Domain.Models
{
    public class Foo1
    {
        [JsonPropertyName("PartnerId")]
        public int CompanyId { get; set; }
        
        [JsonPropertyName("PartnerName")]
        public string CompanyName { get; set; }
        public List<Tracker> Trackers { get; set; }
    }
}