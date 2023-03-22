using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Domain.Models;

namespace Emerson.DataProcessing.Application.Services
{
    public class Foo2Device : IFoo2Device
    {
        private readonly IJsonParser _jsonParser;
        public Foo2Device(IJsonParser jsonParser)
        {
            _jsonParser = jsonParser;
        }
        public async Task<Foo2> Get()
        {
            return await _jsonParser.ParseJson<Foo2>("DeviceDataFoo2.json");
        }
    }
}