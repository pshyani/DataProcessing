namespace Emerson.DataProcessing.Domain.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Crumb> Crumbs { get; set; }
    }
}