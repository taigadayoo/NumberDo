using UnityEngine;

public class InteractableScenario : MonoBehaviour
{
    public Dialogue dialogue; // このオブジェクトに関連する会話データ
    ScenarioDialogue scenarioDialogue;
    public GameObject textBox;

    private bool OneScenario = false;


    void Start()
    {
        scenarioDialogue = FindObjectOfType<ScenarioDialogue>();

        if (!OneScenario&& scenarioDialogue != null)
        {
            textBox.SetActive(true);
            scenarioDialogue.StartDialogue(dialogue); // 触れた際に会話を開始
            OneScenario = true;                               // 
        }

    }
    void OnMouseDown()
    {
       
    }
}