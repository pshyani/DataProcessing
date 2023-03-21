using Emerson.DataProcessing.Domain.Models;

namespace Emerson.DataProcessing.Application.Interfaces
{
    public interface IFoo1Device
    {
         Task<Foo1> Get();
    }
}