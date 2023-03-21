using System.Text.Json;
using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Domain.Models;

namespace Emerson.DataProcessing.Application.Services
{
    public class Foo2Device : IFoo2Device
    {
        public async Task<Foo2> Get()
        {
            using (StreamReader r = new StreamReader("Json/DeviceDataFoo2.json"))
            {
                string json = await r.ReadToEndAsync();
                return JsonSerializer.Deserialize<Foo2>(json);
            }
        }
    }
}