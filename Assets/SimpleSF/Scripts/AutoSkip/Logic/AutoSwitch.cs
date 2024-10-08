using System;

namespace SimpleSFSample
{
    /// <summary>
    /// Mediates in the activation of the auto mode.
    /// </summary>
    public class AutoSwitch : IAutoSwitch
    {
        private readonly IAutoActivator autoActivator;

        public bool IsAutoActive => autoActivator.IsActive;

        public AutoSwitch(IAutoActivator autoActivator)
        {
            this.autoActivator = autoActivator ?? throw new ArgumentNullException(nameof(autoActivator));
        }

        public void SwitchAuto(bool isActive)
        {
            autoActivator.IsActive = isActive;
        }

    }
}