using System;

namespace SimpleSFSample
{
    /// <summary>
    /// The auto mode and the skip mode will never be activated simultaneously.
    /// When the auto mode is enabled, the skip mode will be disabled if it is enabled.
    /// When the skip mode is enabled, the auto mode will be disabled if it is enabled.
    /// </summary>
    public class AutoSkipSwitchExclusiveDecorator : IAutoSwitch, ISkipSwitch
    {
        private readonly IAutoSwitch autoSwitch;
        private readonly ISkipSwitch skipSwitch;

        public bool IsAutoActive => autoSwitch.IsAutoActive;
        public bool IsSkipActive => skipSwitch.IsSkipActive;

        public AutoSkipSwitchExclusiveDecorator(IAutoSwitch autoSwitch, ISkipSwitch skipSwitch)
        {
            this.autoSwitch = autoSwitch ?? throw new ArgumentNullException(nameof(autoSwitch));
            this.skipSwitch = skipSwitch ?? throw new ArgumentNullException(nameof(skipSwitch));
        }

        public void SwitchAuto(bool isActive)
        {
            skipSwitch.SwitchSkip(false);
            autoSwitch.SwitchAuto(isActive);
        }

        public void SwitchSkip(bool isActive)
        {
            autoSwitch.SwitchAuto(false);
            skipSwitch.SwitchSkip(isActive);
        }
    }
}