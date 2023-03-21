using Emerson.DataProcessing.Domain.Models;

namespace Emerson.DataProcessing.Application.Interfaces
{
    public interface IFoo2Device
    {
         Task<Foo2> Get();
    }
}