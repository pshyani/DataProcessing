using System.Text.Json.Serialization;

namespace Emerson.DataProcessing.Domain.Models
{
    public class Device
    {
        [JsonPropertyName("DeviceID")]
        public int DeviceId { get; set; }

        [JsonPropertyName("Name")]
        public string DeviceName { get; set; }
        public string StartDateTime { get; set; }
        public List<SensorDatum> SensorData { get; set; }
    }
}