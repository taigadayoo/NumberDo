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
    public string dialogueText; // 会話内容
}