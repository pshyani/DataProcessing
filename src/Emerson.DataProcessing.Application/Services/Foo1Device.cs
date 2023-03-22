using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Domain.Models;

namespace Emerson.DataProcessing.Application.Services
{
    public class Foo1Device : IFoo1Device
    {
        private readonly IJsonParser _jsonParser;
        public Foo1Device(IJsonParser jsonParser)
        {
            _jsonParser = jsonParser;
        }
        public async Task<Foo1> Get()
        {
            return await _jsonParser.ParseJson<Foo1>("DeviceDataFoo1.json");
        }   
    }
}