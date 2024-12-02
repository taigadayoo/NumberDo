using System;

namespace SimpleSFSample
{
	/// <summary>
	/// When the scenario progression is paused, both the auto mode and the skip mode will be disabled.
	/// </summary>
	public class ScenarioPauserAutoSkipBreakDecorator : IScenarioPauser
	{
		private readonly IScenarioPauser scenarioPauser;
		private readonly IAutoSwitch autoSwitch;
		private readonly ISkipSwitch skipSwitch;

		public ScenarioPauserAutoSkipBreakDecorator(IScenarioPauser scenarioPauser, IAutoSwitch autoSwitch, ISkipSwitch skipSwitch)
		{
			this.scenarioPauser = scenarioPauser ?? throw new ArgumentNullException(nameof(scenarioPauser));
			this.autoSwitch = autoSwitch ?? throw new ArgumentNullException(nameof(autoSwitch));
			this.skipSwitch = skipSwitch ?? throw new ArgumentNullException(nameof(skipSwitch));
		}

		public void Pause()
		{
			autoSwitch.SwitchAuto(false);
			skipSwitch.SwitchSkip(false);
			scenarioPauser.Pause();
		}
	}
}