using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScenarioDialogue : MonoBehaviour
{
    public Text dialogueText; // ��b���e��\������Text�I�u�W�F�N�g
    public Dialogue currentDialogue; // ���ݕ\�����̉�b�f�[�^
    private int currentLineIndex; // ���݂̉�b�s�̃C���f�b�N�X

    SceneManagement sceneManagement;

    SampleSoundManager soundManager;
    void Start()
    {
        soundManager = FindObjectOfType<SampleSoundManager>();
        gameObject.SetActive(true); // ������ԂŔ�\���ɐݒ�
      sceneManagement =   FindObjectOfType<SceneManagement>();
        if(soundManager != null)
        {
            soundManager.PlayBgm(BgmType.BGM2);
        }
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
        sceneManagement.OnGame();
    }

}