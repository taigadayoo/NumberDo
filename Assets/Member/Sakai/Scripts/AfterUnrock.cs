using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterUnrock : MonoBehaviour
{
    [SerializeField]
    ObjectManager objectManager;
    [SerializeField]
   SimpleDialogueManager dialogueManager;
    [SerializeField]
    GameManager gameManager;
    public Dialogue dialogue;
    public GameObject textBox;
    private bool oneUnrock = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objectManager.unrocking && !oneUnrock)
        {
            Unrocking();
            oneUnrock = true;
        }
    }
    private void Unrocking()
    {
        objectManager.Ontext = true;
        objectManager.unrockingRock.SetActive(true);
        gameManager.itemGet = false;
        textBox.SetActive(true);
        dialogueManager.StartDialogue2(dialogue); // ‰ï˜b‚ðŠJŽn
    }
}
