using Emerson.DataProcessing.Application.Models;

namespace Emerson.DataProcessing.Application.Interfaces
{
    public interface ISummarizeData
    {
         Task<IEnumerable<SummarizeDevice>> Get();
    }
}