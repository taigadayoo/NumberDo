namespace SimpleSFSample
{
	/// <summary>
	/// The settings of the auto mode.
	/// </summary>
    public interface IAutoActivator
    {
		/// <summary>
		/// Whetner the auto mode is active.
		/// </summary>
		bool IsActive { get; set; }

		/// <summary>
		/// The number of seconds to wait for before triggering the next-instruction.
		/// </summary>
		float Duration { get; set; }
    }
}