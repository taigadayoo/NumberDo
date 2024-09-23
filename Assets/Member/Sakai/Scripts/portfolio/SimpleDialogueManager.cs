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
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool fullTextDisplayed = false; // 全文が表示されているかどうか
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    
        itemBer = FindObjectOfType<ItemBer>();
        gameObject.SetActive(false); // 初期状態で非表示に設定
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
        gameObject.SetActive(true); // 会話開始時に表示
        DisplayLine();
        objectManager.Ontext = true;
        isTyping = false;
    }
    public void StartDialogue2(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // 会話開始時に表示
        DisplayLine2();
        objectManager.Ontext = true;
        isTyping = false;

    }
    public void StartDialogueItemGeted(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // 会話開始時に表示
        DisplayLine3();
        objectManager.Ontext = true;
        isTyping = false;
    }
    public void StartDialogueFruit(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        gameObject.SetActive(true); // 会話開始時に表示
        DisplayLineFruit();
        objectManager.Ontext = true;
        isTyping = false;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // 左クリックまたはタップ
        {
            if (isTyping)
            {
                // 文字を表示中にクリックされたら全文表示
                SkipToFullLine();
            }
            else if (fullTextDisplayed)
            {
                // 全文が表示された後にクリックされたら次の行に進む
                NextLine();
            }
        }
        //if (Input.GetMouseButtonDown(0)) // 左クリックまたはタップ
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
                DisplayLine(); // 次の行を表示
            }
            else
            {
                // 次の行がない場合、EndDialogueを呼び出す
                EndDialogue();
            }
        }
        if (gameManager.itemGet == false && gameManager.itemGet2 == false )
        {
            if (currentLineIndex < currentDialogue.lines.Count - 1)
            {
                currentLineIndex++;
                DisplayLine2(); // 次の行を表示
            }
            else
            {
                // 次の行がない場合、EndDialogueを呼び出す
                EndDialogue2();
            }
        }
        if (gameManager.itemGet == true && gameManager.itemGet2 == false)
        {
            if (currentLineIndex < currentDialogue.lines.Count - 1)
            {
                currentLineIndex++;
                DisplayLine3(); // 次の行を表示
            }
            else
            {
                // 次の行がない場合、EndDialogueを呼び出す
                EndDialogue2();
            }
        }
        if(gameManager.itemGet == false && gameManager.itemGet2 == true)
        {
            if (currentLineIndex < currentDialogue.lines.Count - 1)
            {
                currentLineIndex++;
                DisplayLineFruit(); // 次の行を表示
            }
            else
            {
                // 次の行がない場合、EndDialogueを呼び出す
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
        objectManager.allColliderSwicth(true);
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
        gameObject.SetActive(false); // 会話終了時に非表示に設定
        objectManager.textEnd = true;
        objectManager.Ontext = false;
    }
}