using ScenarioFlow.Tasks;
using System;

namespace SimpleSFSample
{
    /// <summary>
    /// Mediates in the activation of the skip mode.
    /// </summary>
    public class SkipSwitch : ISkipSwitch
    {
        private readonly ISkipActivator skipActivator;

        public bool IsSkipActive => skipActivator.IsActive;

        public SkipSwitch(ISkipActivator skipActivator)
        {
            this.skipActivator = skipActivator ?? throw new ArgumentNullException(nameof(skipActivator));
        }

        public void SwitchSkip(bool isActive)
        {
            skipActivator.IsActive = isActive;
        }
    }
}