using System.Text.Json;
using Emerson.DataProcessing.Application.Interfaces;

namespace Emerson.DataProcessing.Application.Helper
{
    public class JsonParser : IJsonParser
    {
        public async virtual Task<T> ParseJson<T>(string fileName)
        {
            try
            {
                using (StreamReader r = new StreamReader(string.Concat("Json/", fileName)))
                {
                    string json = await r.ReadToEndAsync();
                    return JsonSerializer.Deserialize<T>(json);
                }
            }
            catch(FileNotFoundException ex)
            {
                throw new FileNotFoundException($"File '{fileName}' is not found", ex);
            }
            catch(Exception ex)
            {
                throw new InvalidDataException($"File '{fileName}' is not in correct format", ex);
            }
        }
    }
}