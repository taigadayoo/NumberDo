using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueendManager : MonoBehaviour
{
    [SerializeField]
    SimpleDialogueManager dialogueManager;
    [SerializeField]
    GameManager gameManager;

    ObjectManager objectManager;
    // Start is called before the first frame update
    public Dialogue dialogue;
    public Dialogue dialogue2;
    public GameObject textBox;

    private bool OneMesse = false;
    void Start()
    {

        objectManager = FindObjectOfType<ObjectManager>();
        StartCoroutine(StartTrueText());
    }

    // Update is called once per frame
    void Update()
    {
        if (objectManager.OnTrueMonitor && objectManager.OnTrueShelse && objectManager.textEnd && !OneMesse)
        {
            StartCoroutine(TouchTrueText());
            OneMesse = true;
        }
    }
    IEnumerator StartTrueText()
    {
        objectManager.textEnd = false;
        yield return new WaitForSeconds(0.001f);
        gameManager.itemGet = false;
        gameManager.itemGet2 = false;
        textBox.SetActive(true);
        dialogueManager.StartDialogue2(dialogue); // �G�ꂽ�ۂɉ�b���J�n
    }
    IEnumerator TouchTrueText()
    {
        objectManager.textEnd = false;
        yield return new WaitForSeconds(0.001f);
        gameManager.itemGet = false;
        gameManager.itemGet2 = false;
        textBox.SetActive(true);
        dialogueManager.StartDialogue2(dialogue2); // �G�ꂽ�ۂɉ�b���J�n
    }
}
