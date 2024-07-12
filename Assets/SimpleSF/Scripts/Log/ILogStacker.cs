namespace SimpleSFSample
{
	/// <summary>
	/// Mediates in adding a new log.
	/// </summary>
	public interface ILogStacker
	{
		/// <summary>
		/// Add a new log.
		/// </summary>
		/// <param name="title">The log title.</param>
		/// <param name="description">The log description.</param>
		void StackLog(string title, string description);
	}
}