using Emerson.DataProcessing.Application.Interfaces;

namespace Emerson.DataProcessing.Application.Services
{
    public class FooDevice : IFooDevice
    {
        private readonly IJsonParser _jsonParser;
        public FooDevice(IJsonParser jsonParser)
        {
            _jsonParser = jsonParser;
        }
        public async Task<T> Get<T>(string deviceDataFile)
        {
            return await _jsonParser.ParseJson<T>(deviceDataFile);
        }
    }
}