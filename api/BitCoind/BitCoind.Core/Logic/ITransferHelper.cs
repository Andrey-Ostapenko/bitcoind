using System.Threading;
using System.Threading.Tasks;

namespace BitCoind.Core.Logic
{
    public interface ITransferHelper
    {
        Task<object> SendBtcAsync(
            ITransactionInfo info,
            CancellationToken ct
        );
    }
}