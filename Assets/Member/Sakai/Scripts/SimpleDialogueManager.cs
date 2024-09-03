using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SimpleDialogueManager : MonoBehaviour
{
    public Text dialogueText; // 会話内容を表示するTextオブジェクト
    public Dialogue currentDialogue; // 現在表示中の会話データ
    private int currentLineIndex; // 現在の会話行のインデックス
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
        gameObject.SetActive(false); // 初期状態で非表示に設定
        chatEnd = false;
        
    }
    
    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // 会話開始時に表示
        DisplayLine();
        objectManager.Ontext = true;
    }
    public void StartDialogue2(Dialogue dialogue)
    {

        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // 会話開始時に表示
        DisplayLine2();
      
    }
    public void StartDialogueItemGeted(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // 会話開始時に表示
        DisplayLine3();
    }
    public void StartDialogueFruit(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // 会話開始時に表示
        DisplayLineFruit();
        objectManager.Ontext = true;
    }
    void Update()
    {
        // タップを検知し、次の会話行に進む
        if (Input.GetMouseButtonDown(0)) // 左クリックまたはタップ
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
            dialogueText.text = line.dialogueText; // 会話内容のみ表示
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
            dialogueText.text = line.dialogueText; // 会話内容のみ表示
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
            dialogueText.text = line.dialogueText; // 会話内容のみ表示
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
            dialogueText.text = line.dialogueText; // 会話内容のみ表示
        }
        else
        {
            EndDialogueFruit();
        }
    }
    public void EndDialogue()
    {
            // 会話終了時の処理
            dialogueText.text = "";
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[objectManager.addItemNum]);
            gameObject.SetActive(false); // 会話終了時に非表示に設定
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
        // 会話終了時の処理
        dialogueText.text = "";    
        gameObject.SetActive(false); // 会話終了時に非表示に設定
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
        gameObject.SetActive(false); // 会話終了時に非表示に設定
        objectManager.textEnd = true;
        objectManager.Ontext = false;
    }
}