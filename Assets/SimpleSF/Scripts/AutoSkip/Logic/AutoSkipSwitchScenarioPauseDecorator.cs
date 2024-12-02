using System;

namespace SimpleSFSample
{
	/// <summary>
	/// Prohibits any activations of the auto mode and the skip mode as long as the scenario progression is paused.
	/// </summary>
	public class AutoSkipSwitchScenarioPauseDecorator : IAutoSwitch, ISkipSwitch
	{
		private readonly IAutoSwitch autoSwitch;
		private readonly ISkipSwitch skipSwitch;
		private readonly IScenarioPauseStatusGetter scenarioPauseStatusGetter;

		public bool IsAutoActive => autoSwitch.IsAutoActive;
		public bool IsSkipActive => skipSwitch.IsSkipActive;

		public AutoSkipSwitchScenarioPauseDecorator(IAutoSwitch autoSwitch, ISkipSwitch skipSwitch, IScenarioPauseStatusGetter scenarioPauseStatusGetter)
		{
			this.autoSwitch = autoSwitch ?? throw new ArgumentNullException(nameof(autoSwitch));
			this.skipSwitch = skipSwitch ?? throw new ArgumentNullException(nameof(skipSwitch));
			this.scenarioPauseStatusGetter = scenarioPauseStatusGetter ?? throw new ArgumentNullException(nameof(scenarioPauseStatusGetter));
		}

		public void SwitchAuto(bool isActive)
		{
			autoSwitch.SwitchAuto(isActive && !scenarioPauseStatusGetter.IsPaused);
		}

		public void SwitchSkip(bool isActive)
		{
			skipSwitch.SwitchSkip(isActive && !scenarioPauseStatusGetter.IsPaused);
		}
	}
}