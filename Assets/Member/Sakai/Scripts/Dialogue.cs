using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<DialogueLine> lines; // 複数行の会話をサポート
}

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 10)] // インスペクターで複数行の入力をサポート
    public string dialogueText; // 会話内容
}