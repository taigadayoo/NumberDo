namespace SimpleSFSample
{
    /// <summary>
    /// Meidate the activation of the skip mode.
    /// </summary>
    public interface ISkipSwitch
    {
        /// <summary>
        /// Whether the skip mode is active.
        /// </summary>
        bool IsSkipActive { get; }
        /// <summary>
        /// Trun on/off the skip mode.
        /// </summary>
        /// <param name="isActive"></param>
        void SwitchSkip(bool isActive);
    }
}