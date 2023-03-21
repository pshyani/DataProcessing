using Emerson.DataProcessing.Application.Models;
using Emerson.DataProcessing.Domain.Models;

namespace Emerson.DataProcessing.Application.Interfaces
{
    public interface ISummarizeData
    {
         Task<IEnumerable<SummarizeDevice>> Get();
    }
}