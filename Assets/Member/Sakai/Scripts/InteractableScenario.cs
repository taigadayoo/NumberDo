using UnityEngine;

public class InteractableScenario : MonoBehaviour
{
    public Dialogue dialogue; // ���̃I�u�W�F�N�g�Ɋ֘A�����b�f�[�^
    ScenarioDialogue scenarioDialogue;
    public GameObject textBox;

    private bool OneScenario = false;


    void Start()
    {
        scenarioDialogue = FindObjectOfType<ScenarioDialogue>();

        if (!OneScenario&& scenarioDialogue != null)
        {
            textBox.SetActive(true);
            scenarioDialogue.StartDialogue(dialogue); // �G�ꂽ�ۂɉ�b���J�n
            OneScenario = true;                               // 
        }

    }
    void OnMouseDown()
    {
       
    }
}