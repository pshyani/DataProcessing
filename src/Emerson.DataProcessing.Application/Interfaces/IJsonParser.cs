using Emerson.DataProcessing.Domain.Models;

namespace Emerson.DataProcessing.Application.Interfaces
{
    public interface IJsonParser
    {
         Task<T> ParseJson<T>(string fileName);
    }
}