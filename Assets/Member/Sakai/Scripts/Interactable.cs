using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Dialogue dialogue; // ���̃I�u�W�F�N�g�Ɋ֘A�����b�f�[�^
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
            dialogueManager.StartDialogue(dialogue); // �G�ꂽ�ۂɉ�b���J�n
        }
    }
}