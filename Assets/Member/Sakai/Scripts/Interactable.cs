using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Dialogue dialogue; // ���̃I�u�W�F�N�g�Ɋ֘A�����b�f�[�^
    private SimpleDialogueManager dialogueManager;
    public GameObject textBox;
    ObjectManager objectManager;
    GameManager gameManager;

    public enum TouchAction
    {
       
        noItem,
        itemGet,
        itemGeted
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
        if (dialogueManager != null && dialogue != null  && touchAction == TouchAction.itemGet && !objectManager.Ontext)
        {
            gameManager.itemGet = true;
            textBox.SetActive(true);
            dialogueManager.StartDialogue(dialogue); // �G�ꂽ�ۂɉ�b���J�n
            touchAction = TouchAction.itemGeted;
        }
        if (dialogueManager != null && dialogue != null && touchAction == TouchAction.itemGeted && !objectManager.Ontext)
        {
            textBox.SetActive(true);
            dialogueManager.StartDialogueItemGeted(dialogue); // �G�ꂽ�ۂɉ�b���J�n
            gameManager.itemGet = null;
          
        }
        if (dialogueManager != null && dialogue != null  && touchAction == TouchAction.noItem && !objectManager.Ontext)
        {
            gameManager.itemGet = false;
            textBox.SetActive(true);
            dialogueManager.StartDialogue2(dialogue); // �G�ꂽ�ۂɉ�b���J�n
        }
    }
}