using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using ScenarioFlow.Tasks;
using System;
using System.Threading;

namespace SimpleSFSample
{
	/// <summary>
	/// Stop the scenario progression while it is paused.
	/// </summary>
	public class ScenarioTaskExecutorScenarioPauseDecorator : IScenarioTaskExecutor
	{
		private readonly IScenarioTaskExecutor scenarioTaskExecutor;
		private readonly ITokenCodeGetter tokenCodeGetter;
		private readonly IScenarioPauseStatusGetter scenarioPauseStatusGetter;

		public ScenarioTaskExecutorScenarioPauseDecorator(IScenarioTaskExecutor scenarioTaskExecutor, ITokenCodeGetter tokenCodeGetter, IScenarioPauseStatusGetter scenarioPauseStatusGetter)
		{
			this.scenarioTaskExecutor = scenarioTaskExecutor ?? throw new ArgumentNullException(nameof(scenarioTaskExecutor));
			this.tokenCodeGetter = tokenCodeGetter ?? throw new ArgumentNullException(nameof(tokenCodeGetter));
			this.scenarioPauseStatusGetter = scenarioPauseStatusGetter ?? throw new ArgumentNullException(nameof(scenarioPauseStatusGetter));
		}

		public async UniTask ExecuteAsync(UniTask scenarioTask, CancellationToken cancellationToken)
		{
			await scenarioTaskExecutor.ExecuteAsync(scenarioTask, cancellationToken);
			if (scenarioPauseStatusGetter.IsPaused && (SpecialTokenCodes.IsStandard(tokenCodeGetter.TokenCode) || SpecialTokenCodes.IsFluent(tokenCodeGetter.TokenCode)))
			{
				//Wait until the scenario progression is unlocked
				await UniTaskAsyncEnumerable.EveryValueChanged(scenarioPauseStatusGetter, x => x.IsPaused)
					.Where(x => !x)
					.FirstOrDefaultAsync(cancellationToken: cancellationToken);
			}
		}
	}
}