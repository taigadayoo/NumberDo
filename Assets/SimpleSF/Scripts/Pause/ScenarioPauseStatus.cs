namespace SimpleSFSample
{
	/// <summary>
	/// Manages the pause status of the scenario progression.
	/// </summary>
	public class ScenarioPauseStatus : IScenarioPauser, IScenarioResumer, IScenarioPauseStatusGetter
	{
		public bool IsPaused { get; private set; }

		public void Pause()
		{
			IsPaused = true;
		}

		public void Resume()
		{
			IsPaused = false;
		}
	}
}