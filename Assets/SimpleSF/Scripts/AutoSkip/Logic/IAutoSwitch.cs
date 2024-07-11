namespace SimpleSFSample
{
    /// <summary>
    /// Mediates in the activation of the auto mode.
    /// </summary>
    public interface IAutoSwitch
    {
        /// <summary>
        /// Whether the auto mode is active.
        /// </summary>
        bool IsAutoActive { get; }
        /// <summary>
        /// Turn on/off the auto mode.
        /// </summary>
        /// <param name="isActive"></param>
        void SwitchAuto(bool isActive);
    }
}