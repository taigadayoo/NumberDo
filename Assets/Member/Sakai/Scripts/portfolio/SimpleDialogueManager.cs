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
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool fullTextDisplayed = false; // �S�����\������Ă��邩�ǂ���
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    
        itemBer = FindObjectOfType<ItemBer>();
        gameObject.SetActive(false); // ������ԂŔ�\���ɐݒ�
        chatEnd = false;
        isTyping = false;
        fullTextDisplayed = false;
    }
    IEnumerator TypeText(string text)
    {
        isTyping = true;
        fullTextDisplayed = false;
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        fullTextDisplayed = true;
    }
    void SkipToFullLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        var line = currentDialogue.lines[currentLineIndex];
        dialogueText.text = line.dialogueText;
        isTyping = false;
        fullTextDisplayed = true;
    }
    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // ��b�J�n���ɕ\��
        DisplayLine();
        objectManager.Ontext = true;
        isTyping = false;
    }
    public void StartDialogue2(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // ��b�J�n���ɕ\��
        DisplayLine2();
        objectManager.Ontext = true;
        isTyping = false;

    }
    public void StartDialogueItemGeted(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // ��b�J�n���ɕ\��
        DisplayLine3();
        objectManager.Ontext = true;
        isTyping = false;
    }
    public void StartDialogueFruit(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // ��b�J�n���ɕ\��
        DisplayLineFruit();
        objectManager.Ontext = true;
        isTyping = false;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // ���N���b�N�܂��̓^�b�v
        {
            if (isTyping)
            {
                // ������\�����ɃN���b�N���ꂽ��S���\��
                SkipToFullLine();
            }
            else if (fullTextDisplayed)
            {
                // �S�����\�����ꂽ��ɃN���b�N���ꂽ�玟�̍s�ɐi��
                NextLine();
            }
        }
        //if (Input.GetMouseButtonDown(0)) // ���N���b�N�܂��̓^�b�v
        //{
        //    NextLine();
        //}
        if (this.gameObject.activeSelf)
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
            if (currentLineIndex < currentDialogue.lines.Count - 1)
            {
                currentLineIndex++;
                DisplayLine(); // ���̍s��\��
            }
            else
            {
                // ���̍s���Ȃ��ꍇ�AEndDialogue���Ăяo��
                EndDialogue();
            }
        }
        if (gameManager.itemGet == false && gameManager.itemGet2 == false )
        {
            if (currentLineIndex < currentDialogue.lines.Count - 1)
            {
                currentLineIndex++;
                DisplayLine2(); // ���̍s��\��
            }
            else
            {
                // ���̍s���Ȃ��ꍇ�AEndDialogue���Ăяo��
                EndDialogue2();
            }
        }
        if (gameManager.itemGet == true && gameManager.itemGet2 == false)
        {
            if (currentLineIndex < currentDialogue.lines.Count - 1)
            {
                currentLineIndex++;
                DisplayLine3(); // ���̍s��\��
            }
            else
            {
                // ���̍s���Ȃ��ꍇ�AEndDialogue���Ăяo��
                EndDialogue2();
            }
        }
        if(gameManager.itemGet == false && gameManager.itemGet2 == true)
        {
            if (currentLineIndex < currentDialogue.lines.Count - 1)
            {
                currentLineIndex++;
                DisplayLineFruit(); // ���̍s��\��
            }
            else
            {
                // ���̍s���Ȃ��ꍇ�AEndDialogue���Ăяo��
                EndDialogueFruit();
            }
        }
    }
    public void DisplayLine()
    {
        if (currentLineIndex < currentDialogue.lines.Count)
        {
            var line = currentDialogue.lines[currentLineIndex];
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(line.dialogueText));
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
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(line.dialogueText));
        }
        else
        {
            EndDialogue2();
        }
    }
    public void DisplayLine3()
    {
        if (currentLineIndex < 2)
        {
            var line = currentDialogue.lines[currentLineIndex];
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(line.dialogueText));
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
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(line.dialogueText));
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
        objectManager.allColliderSwicth(true);
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
        if (!objectManager.OnBox4)
        {
            objectManager.allColliderSwicth(true);
        }
    }
    public void EndDialogueFruit()
    {
        dialogueText.text = "";
        
        StartCoroutine(FruitTouch());
        objectManager.allColliderSwicth(true);

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