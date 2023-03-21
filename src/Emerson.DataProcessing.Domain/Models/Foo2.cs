using System.Text.Json.Serialization;

namespace Emerson.DataProcessing.Domain.Models
{
    public class Foo2
    {
        public int CompanyId { get; set; }

        [JsonPropertyName("Company")]
        public string CompanyName { get; set; }
        
        public List<Device> Devices { get; set; }
    }
}