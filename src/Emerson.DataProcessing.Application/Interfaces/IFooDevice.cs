
namespace Emerson.DataProcessing.Application.Interfaces
{
    public interface IFooDevice
    {
         Task<T> Get<T>(string deviceDataFile);
    }   
}   