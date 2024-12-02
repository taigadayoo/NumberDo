using System.Threading;

namespace SimpleSFSample
{
    /// <summary>
    /// Requires an initialization with a cancellation token.
    /// </summary>
    public interface ICancellationInitializable
    {
        void InitializeWithCancellation(CancellationToken cancellationToken);
    }
}