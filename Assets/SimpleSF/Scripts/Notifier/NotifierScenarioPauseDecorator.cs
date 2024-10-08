using Cysharp.Threading.Tasks;
using ScenarioFlow.Tasks;
using System;
using System.Threading;

namespace SimpleSFSample
{
	/// <summary>
	/// A notifier that ignores the next/cancellation-instruction as long as the scenario progression is paused.
	/// </summary>
	public class NotifierScenarioPauseDecorator : INextNotifier, ICancellationNotifier
	{
		private readonly INextNotifier nextNotifier;
		private readonly ICancellationNotifier cancellationNotifier;
		private readonly IScenarioPauseStatusGetter scenarioPauseStatusGetter;

		public NotifierScenarioPauseDecorator(INextNotifier nextNotifier, ICancellationNotifier cancellationNotifier, IScenarioPauseStatusGetter scenarioPauseStatusGetter)
		{
			this.nextNotifier = nextNotifier ?? throw new ArgumentNullException(nameof(nextNotifier));
			this.cancellationNotifier = cancellationNotifier ?? throw new ArgumentNullException(nameof(cancellationNotifier));
			this.scenarioPauseStatusGetter = scenarioPauseStatusGetter ?? throw new ArgumentNullException(nameof(scenarioPauseStatusGetter));
		}

		public async UniTask NotifyNextAsync(CancellationToken cancellationToken)
		{
			do
			{
				await nextNotifier.NotifyNextAsync(cancellationToken);
			}
			while (scenarioPauseStatusGetter.IsPaused);
		}

		public async UniTask NotifyCancellationAsync(CancellationToken cancellationToken)
		{
			do
			{
				await cancellationNotifier.NotifyCancellationAsync(cancellationToken);
			}
			while (scenarioPauseStatusGetter.IsPaused);
		}
	}
}