using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleSFSample
{
	/// <summary>
	/// Stores scenario logs and shows them.
	/// </summary>
	public class LogSupervisor : ILogStacker, ICancellationInitializable
	{
		private static readonly int DisplayCount = 5;

		private readonly IScenarioPauser scenarioPauser;
		private readonly IScenarioResumer scenarioResumer;

		private readonly GameObject logPanelObject;
		private readonly Button openButton;
		private readonly Button closeButton;
		private readonly GameObject logObjectParent;
		private readonly LogObject logObjectPrefab;
		private readonly Slider logSlider;
		private readonly Button upButton;
		private readonly Button downButton;

		private readonly List<Log> logList = new List<Log>();

		private LogObject[] logObjects;

		public LogSupervisor(IScenarioPauser scenarioPauser, IScenarioResumer scenarioResumer, Settings settings)
		{
			this.scenarioPauser = scenarioPauser ?? throw new ArgumentNullException(nameof(scenarioPauser));
			this.scenarioResumer = scenarioResumer ?? throw new ArgumentNullException(nameof(scenarioResumer));

			logPanelObject = settings.LogPanelObject ?? throw new ArgumentNullException(nameof(settings.LogPanelObject));
			openButton = settings.OpenButton ?? throw new ArgumentNullException(nameof(settings.OpenButton));
			closeButton = settings.CloseButton ?? throw new ArgumentNullException(nameof(settings.CloseButton));
			logObjectParent = settings.LogObjectParent ?? throw new ArgumentNullException(nameof(settings.LogObjectParent));
			logObjectPrefab = settings.LogObjectPrefab ?? throw new ArgumentNullException(nameof(settings.LogObjectPrefab));
			logSlider = settings.LogSlider ?? throw new ArgumentNullException(nameof(settings.LogSlider));
			upButton = settings.UpButton ?? throw new ArgumentNullException(nameof(settings.UpButton));
			downButton = settings.DownButotn ?? throw new ArgumentNullException(nameof(settings.DownButotn));

			logPanelObject.transform.position = Vector3.zero;
			logPanelObject.SetActive(false);
			openButton.interactable = true;
			closeButton.interactable = false;
		}

		public void InitializeWithCancellation(CancellationToken cancellationToken)
		{
			openButton.OnClickAsAsyncEnumerable(cancellationToken: cancellationToken)
				.ForEachAsync(_ => OpenLog());
			closeButton.OnClickAsAsyncEnumerable(cancellationToken: cancellationToken)
				.ForEachAsync(_ => CloseLog());
		}

		public void StackLog(string title, string description)
		{
			logList.Add(new Log { Title = title, Description = description });
		}

		private void OpenLog()
		{
			scenarioPauser.Pause();
			logPanelObject.SetActive(true);
			openButton.interactable = false;
			closeButton.interactable = true;

			logObjects = Enumerable.Range(0, DisplayCount).Select(_ => GameObject.Instantiate(logObjectPrefab, logObjectParent.transform)).ToArray();

			var cancellationToken = logObjects[0].GetCancellationTokenOnDestroy();
			var maxOffset = logList.Count > DisplayCount ? logList.Count - DisplayCount : 0;
			logSlider.minValue = 0;
			logSlider.maxValue = maxOffset;
			logSlider.value = 0;
			Update();
			//Slider
			logSlider.OnValueChangedAsAsyncEnumerable(cancellationToken: cancellationToken)
				.ForEachAsync(_ => Update(), cancellationToken: cancellationToken)
			.Forget();
			//Up button
			upButton.OnClickAsAsyncEnumerable(cancellationToken: cancellationToken)
				.ForEachAsync(_ =>
				{
					logSlider.value = Mathf.FloorToInt(logSlider.value) + 1;
					Update();
				}, cancellationToken: cancellationToken).Forget();
			//Down button
			downButton.OnClickAsAsyncEnumerable(cancellationToken: cancellationToken)
				.ForEachAsync(_ =>
				{
					logSlider.value = Mathf.FloorToInt(logSlider.value) - 1;
					Update();
				}, cancellationToken: cancellationToken).Forget();

			void Update()
			{
				var pickedLogs = logList.Count > DisplayCount ? logList.Skip(logList.Count - DisplayCount - Mathf.FloorToInt(logSlider.value)).Take(DisplayCount).ToList() : logList;
				foreach (var index in Enumerable.Range(0, DisplayCount))
				{
					var logObject = logObjects[index];
					if (index < pickedLogs.Count)
					{
						var log = pickedLogs[index];
						logObject.SetText(log.Title, log.Description);
					}
					else
					{
						logObject.SetText(string.Empty, string.Empty);
					}
				}
			}
		}

		private void CloseLog()
		{
			scenarioResumer.Resume();
			logPanelObject.SetActive(false);
			openButton.interactable = true;
			closeButton.interactable = false;
			if (logObjects != null)
			{
				foreach (var logObject in logObjects)
				{
					GameObject.Destroy(logObject.gameObject);
				}
				logObjects = null;
			}
		}

		public class Log
		{
			public string Title;
			public string Description;
		}

		[Serializable]
		public class Settings
		{
			public GameObject LogPanelObject;
			public Button OpenButton;
			public Button CloseButton;
			public GameObject LogObjectParent;
			public LogObject LogObjectPrefab;
			public Slider LogSlider;
			public Button UpButton;
			public Button DownButotn;
		}
	}
}