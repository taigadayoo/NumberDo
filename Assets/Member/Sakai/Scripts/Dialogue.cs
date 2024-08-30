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
    [TextArea(3, 10)] // �C���X�y�N�^�[�ŕ����s�̓��͂��T�|�[�g
    public string dialogueText; // ��b���e
}