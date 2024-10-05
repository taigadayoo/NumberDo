using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    public Dialogue dialogue; // このオブジェクトに関連する会話データ
 [SerializeField]
     SimpleDialogueManager dialogueManager;
    public GameObject textBox;
    ObjectManager objectManager;
    GameManager gameManager;
    Timer timer;
    public enum TouchAction
    {
       
        noItem,
        itemGet,
        itemGeted,
        fruit
    }
    [SerializeField]
   public TouchAction touchAction;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
            gameManager = FindObjectOfType<GameManager>();
        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<SimpleDialogueManager>();
        }
        
            objectManager = FindObjectOfType<ObjectManager>();
    }
    private void Update()
    {
        
    }
    void OnMouseDown()
    {
       
        if (!objectManager.ItemGet)
        {
            objectManager.textEnd = false;
          
            if (dialogueManager != null && dialogue != null && touchAction == TouchAction.itemGet && !objectManager.Ontext )
            {
                gameManager.itemGet = true;
                gameManager.itemGet2 = true;
                textBox.SetActive(true);
                dialogueManager.StartDialogue(dialogue); // 触れた際に会話を開始
                touchAction = TouchAction.itemGeted;
            }
            if (dialogueManager != null && dialogue != null && touchAction == TouchAction.itemGeted && !objectManager.Ontext)
            {
                textBox.SetActive(true);
                dialogueManager.StartDialogueItemGeted(dialogue); // 触れた際に会話を開始
                gameManager.itemGet = true;
                gameManager.itemGet2 = false;

            }
            if (dialogueManager != null && dialogue != null && touchAction == TouchAction.fruit && !objectManager.Ontext)
            {
                textBox.SetActive(true);
                dialogueManager.StartDialogueFruit(dialogue); // 触れた際に会話を開始
                gameManager.itemGet = false;
                gameManager.itemGet2 = true;
                touchAction = TouchAction.itemGeted;
            }

            if (dialogueManager != null && dialogue != null && touchAction == TouchAction.noItem && !objectManager.Ontext)
            {
                if ( gameObject.tag == "Monitor" && objectManager.OnKeyCode)
                {
                   
                }
                else
                {
                    gameManager.itemGet = false;
                    gameManager.itemGet2 = false;
                    textBox.SetActive(true);
                    dialogueManager.StartDialogue2(dialogue); // 触れた際に会話を開始
                    if (gameObject.tag == "MiniGame")
                    {
                        objectManager.OnMiniGame = true;
                        objectManager.OnKeyCode = true;
                    }
                }
            }
        }
    }
}