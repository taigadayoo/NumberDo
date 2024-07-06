using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<DialogueLine> lines; // �����s�̉�b���T�|�[�g
}

[System.Serializable]
public class DialogueLine
{
    public string dialogueText; // ��b���e
}