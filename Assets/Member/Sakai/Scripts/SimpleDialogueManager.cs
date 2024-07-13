using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SimpleDialogueManager : MonoBehaviour
{
    public Text dialogueText; // ��b���e��\������Text�I�u�W�F�N�g
    public Dialogue currentDialogue; // ���ݕ\�����̉�b�f�[�^
    private int currentLineIndex; // ���݂̉�b�s�̃C���f�b�N�X
    public bool chatEnd = false;
    ObjectManager objectManager;
    [SerializeField]
    ItemGetSet getSet;
    ItemBer itemBer;

    
    GameManager gameManager;
    public event Action OnChatEnd;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objectManager = FindFirstObjectByType<ObjectManager>();
        itemBer = FindObjectOfType<ItemBer>();
        gameObject.SetActive(false); // ������ԂŔ�\���ɐݒ�
        chatEnd = false;
        
    }
    
    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // ��b�J�n���ɕ\��
        DisplayLine();
        objectManager.Ontext = true;
    }
    public void StartDialogue2(Dialogue dialogue)
    {

        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // ��b�J�n���ɕ\��
        DisplayLine2();
      
    }
    public void StartDialogueItemGeted(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // ��b�J�n���ɕ\��
        DisplayLine3();
    }
    void Update()
    {
        // �^�b�v�����m���A���̉�b�s�ɐi��
        if (Input.GetMouseButtonDown(0)) // ���N���b�N�܂��̓^�b�v
        {
            NextLine();
        }
        if(this.gameObject.activeSelf)
        {
            objectManager.Ontext = true;
        }
        else
        {
            objectManager.Ontext = false;
        }
    }

    public void NextLine()
    {
        if (gameManager.itemGet == true)
        {
            DisplayLine();
            currentLineIndex++;
        }
        if (gameManager.itemGet == false)
        {
            DisplayLine2();
            currentLineIndex++;
        }
        if(gameManager.itemGet == null)
        {
            DisplayLine3();
            currentLineIndex++;
        }
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
    public void DisplayLine2()
    {
        if (currentLineIndex < currentDialogue.lines.Count)
        {
            var line = currentDialogue.lines[currentLineIndex];
            dialogueText.text = line.dialogueText; // ��b���e�̂ݕ\��
        }
        else
        {
            EndDialogue2();
        }
    }
    public void DisplayLine3()
    {
        if (currentLineIndex <1)
        {
            var line = currentDialogue.lines[currentLineIndex];
            dialogueText.text = line.dialogueText; // ��b���e�̂ݕ\��
        }
        else
        {
            EndDialogue2();
        }
    }
    public void EndDialogue()
    {
        // ��b�I�����̏���
        dialogueText.text = "";
        getSet.ImageChange(objectManager.imageNum);
        itemBer.AddItem(objectManager.items[objectManager.addItemNum]);
        gameObject.SetActive(false); // ��b�I�����ɔ�\���ɐݒ�

    }
    public void EndDialogue2()
    {
        //Debug.Log("asaa");
        // ��b�I�����̏���
        dialogueText.text = "";    
        gameObject.SetActive(false); // ��b�I�����ɔ�\���ɐݒ�
        objectManager.Ontext = false;
    }
    
}