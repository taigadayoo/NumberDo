using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Dialogue dialogue; // このオブジェクトに関連する会話データ
    private SimpleDialogueManager dialogueManager;
    public GameObject textBox;
    ObjectManager objectManager;
    GameManager gameManager;

    private bool getedItem = false;
    public enum TouchAction
    {
       
        noItem,
        itemGet,
    }
    [SerializeField]
    TouchAction touchAction;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dialogueManager = FindObjectOfType<SimpleDialogueManager>();
        objectManager = FindObjectOfType<ObjectManager>();
    }
    void OnMouseDown()
    {
        if (dialogueManager != null && dialogue != null  && touchAction == TouchAction.itemGet && !objectManager.Ontext && !getedItem)
        {
            gameManager.itemGet = true;
            textBox.SetActive(true);
            dialogueManager.StartDialogue(dialogue); // 触れた際に会話を開始
            getedItem = true;
        }
        if (dialogueManager != null && dialogue != null && touchAction == TouchAction.itemGet && !objectManager.Ontext && getedItem)
        {
            gameManager.itemGet = true;
            textBox.SetActive(true);
            dialogueManager.StartDialogueItemGeted(dialogue); // 触れた際に会話を開始
          
        }
        if (dialogueManager != null && dialogue != null  && touchAction == TouchAction.noItem && !objectManager.Ontext)
        {
            gameManager.itemGet = false;
            textBox.SetActive(true);
            dialogueManager.StartDialogue2(dialogue); // 触れた際に会話を開始
        }
    }
}