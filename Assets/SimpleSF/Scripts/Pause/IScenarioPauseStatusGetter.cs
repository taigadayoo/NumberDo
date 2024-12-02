namespace SimpleSFSample
{
	/// <summary>
	/// Provides whether the scenario progression is being paused
	/// </summary>
	public interface IScenarioPauseStatusGetter
	{
		/// <summary>
		/// Whether the scenario progression is being paused.
		/// </summary>
		bool IsPaused { get; }
	}
}