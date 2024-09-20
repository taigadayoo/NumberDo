using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueendManager : MonoBehaviour
{
    [SerializeField]
    SimpleDialogueManager dialogueManager;
    [SerializeField]
    GameManager gameManager;
    // Start is called before the first frame update
    public Dialogue dialogue;
    public GameObject textBox;
    void Start()
    {
        StartCoroutine(StartTrueText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartTrueText()
    {
        yield return new WaitForSeconds(0.001f);
        gameManager.itemGet = false;
        gameManager.itemGet2 = false;
        textBox.SetActive(true);
        dialogueManager.StartDialogue2(dialogue); // êGÇÍÇΩç€Ç…âÔòbÇäJén
    }
}
