using System.Text.Json;
using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Domain.Models;

namespace Emerson.DataProcessing.Application.Services
{
    public class Foo1Device : IFoo1Device
    {
        public async Task<Foo1> Get()
        {
            using (StreamReader r = new StreamReader("Json/DeviceDataFoo1.json"))
            {
                string json = await r.ReadToEndAsync();
                return JsonSerializer.Deserialize<Foo1>(json);
            }
        }   
    }
}