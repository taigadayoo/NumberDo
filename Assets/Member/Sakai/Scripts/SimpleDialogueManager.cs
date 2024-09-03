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
    [SerializeField]
    ObjectManager objectManager;
    [SerializeField]
    ItemGetSet getSet;
    ItemBer itemBer;

    GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    
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
    public void StartDialogueFruit(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // ��b�J�n���ɕ\��
        DisplayLineFruit();
        objectManager.Ontext = true;
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
        if (gameManager.itemGet == true && gameManager.itemGet2 == true)
        {
            DisplayLine();
            currentLineIndex++;
        }
        if (gameManager.itemGet == false && gameManager.itemGet2 == false )
        {
            DisplayLine2();
            currentLineIndex++;
        }
        if (gameManager.itemGet == true && gameManager.itemGet2 == false)
        {
            DisplayLine3();
            currentLineIndex++;
        }
        if(gameManager.itemGet == false && gameManager.itemGet2 == true)
        {
            DisplayLineFruit();
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
    public void DisplayLineFruit()
    {
        if (currentLineIndex < currentDialogue.lines.Count)
        {
            var line = currentDialogue.lines[currentLineIndex];
            dialogueText.text = line.dialogueText; // ��b���e�̂ݕ\��
        }
        else
        {
            EndDialogueFruit();
        }
    }
    public void EndDialogue()
    {
            // ��b�I�����̏���
            dialogueText.text = "";
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[objectManager.addItemNum]);
            gameObject.SetActive(false); // ��b�I�����ɔ�\���ɐݒ�
            objectManager.textEnd = true;
          if(objectManager.OnClock)
        {
            objectManager.clock.SetActive(false);
        }
        if (objectManager.OnBomb)
        {
            
            objectManager.bombUnrock.SetActive(false);
        }
        objectManager.Ontext = false;
    }
    public void EndDialogue2()
    {
        //Debug.Log("asaa");
        // ��b�I�����̏���
        dialogueText.text = "";    
        gameObject.SetActive(false); // ��b�I�����ɔ�\���ɐݒ�
        objectManager.Ontext = false;
        objectManager.textEnd = true;
        if(objectManager.OnGoal)
        {
            if(objectManager.OnHaveBomb)
            {
                SceneManagement.Instance.OnClear();
                SampleSoundManager.Instance.StopBgm();
            }
            if(!objectManager.OnHaveBomb)
            {
                SceneManagement.Instance.OnGameOver2();
                SampleSoundManager.Instance.StopBgm();
            }
        }
    }
    public void EndDialogueFruit()
    {
        dialogueText.text = "";
        
        StartCoroutine(FruitTouch());


    }
    IEnumerator FruitTouch()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        getSet.ImageChange(20);
        itemBer.AddItem(objectManager.items[3]);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        getSet.ImageChange(13);
        itemBer.AddItem(objectManager.items[13]);
        gameObject.SetActive(false); // ��b�I�����ɔ�\���ɐݒ�
        objectManager.textEnd = true;
        objectManager.Ontext = false;
    }
}