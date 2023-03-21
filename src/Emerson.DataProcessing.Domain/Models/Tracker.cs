using System.Text.Json.Serialization;

namespace Emerson.DataProcessing.Domain.Models
{
    public class Tracker
    {
        [JsonPropertyName("Id")]
        public int DeviceId { get; set; }

        [JsonPropertyName("Model")]
        public string DeviceName { get; set; }
        public string ShipmentStartDtm { get; set; }
        public List<Sensor> Sensors { get; set; }
    }
}