using System.Threading;
using System.Threading.Tasks;

namespace BitCoind.Core.Logic
{
    public interface IHistoryHelper
    {
        Task<object> GetLastAsync(CancellationToken cancellationToken);
    }
}