using UnityEngine;
using UnityEngine.UI;
using System;

public class SimpleDialogueManager : MonoBehaviour
{
    public Text dialogueText; // ��b���e��\������Text�I�u�W�F�N�g
    public Dialogue currentDialogue; // ���ݕ\�����̉�b�f�[�^
    private int currentLineIndex; // ���݂̉�b�s�̃C���f�b�N�X
    public bool chatEnd = false;

    public event Action OnChatEnd;
    void Start()
    {
        gameObject.SetActive(false); // ������ԂŔ�\���ɐݒ�
        chatEnd = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // ��b�J�n���ɕ\��
        DisplayLine();
    }
    void Update()
    {
        // �^�b�v�����m���A���̉�b�s�ɐi��
        if (Input.GetMouseButtonDown(0)) // ���N���b�N�܂��̓^�b�v
        {
            NextLine();
        }
    }

    public void NextLine()
    {
     
        DisplayLine();
        currentLineIndex++;
    }
    public void DisplayLine()
    {
        if (currentLineIndex < currentDialogue.lines.Count)
        {
            var line = currentDialogue.lines[currentLineIndex];
            dialogueText.text = line.dialogueText; // ��b���e�̂ݕ\��
        }
        else
        {
            EndDialogue();
        }
    }
 
    public void EndDialogue()
    {
        // ��b�I�����̏���
        dialogueText.text = "";
        gameObject.SetActive(false); // ��b�I�����ɔ�\���ɐݒ�
        chatEnd = true;

    }
}