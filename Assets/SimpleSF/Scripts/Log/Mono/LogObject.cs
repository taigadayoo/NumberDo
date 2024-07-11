using TMPro;
using UnityEngine;

/// <summary>
/// An object with the texts about a log.
/// </summary>
public class LogObject : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI titleText;
	[SerializeField]
	private TextMeshProUGUI descriptionText;

	public void SetText(string title, string description)
	{
		titleText.text = title;
		descriptionText.text = description;
	}
}
