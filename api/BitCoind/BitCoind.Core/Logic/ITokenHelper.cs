using System.Threading;
using System.Threading.Tasks;

namespace BitCoind.Core.Logic
{
    public interface ITokenHelper
    {
        Task<ITokenInfo> Check(
            string token,
            CancellationToken ct = default(CancellationToken));
    }
}