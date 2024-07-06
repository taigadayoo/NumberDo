using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Dialogue dialogue; // このオブジェクトに関連する会話データ
    private SimpleDialogueManager dialogueManager;
    public GameObject textBox;

    void Start()
    {
        dialogueManager = FindObjectOfType<SimpleDialogueManager>();
    }
    void OnMouseDown()
    {
        if (dialogueManager != null && dialogue != null)
        {
            textBox.SetActive(true);
            dialogueManager.StartDialogue(dialogue); // 触れた際に会話を開始
        }
    }
}