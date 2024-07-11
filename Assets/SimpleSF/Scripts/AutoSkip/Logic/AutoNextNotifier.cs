using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using ScenarioFlow.Tasks;
using System;
using System.Linq;
using System.Threading;

namespace SimpleSFSample
{
    /// <summary>
    /// Triggers the next instruction automatically in the specified seconds.
    /// </summary>
    public class AutoNextNotifier : INextNotifier, IAutoActivator
    {
        public bool IsActive { get; set; } = false;
        public float Duration { get; set; } = 1.0f;

        public async UniTask NotifyNextAsync(CancellationToken cancellationToken)
        {
            //Wait for the specified seconds
            await UniTask.Delay(TimeSpan.FromSeconds(Duration), cancellationToken: cancellationToken);
            //If the auto mode is not active, then wait until it is activated
            if (!IsActive)
            {
                await UniTaskAsyncEnumerable.EveryValueChanged(this, x => x.IsActive)
                    .Where(x => x)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            }
        }
    }
}