using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerScripts : MonoBehaviour
{
    public GameObject anim;
    ObjectManager objectManager;
    SceneManagement sceneManagement;
    public GameObject bikkuri;
    public GameObject textBox;
    GameManager gameManager;
    [SerializeField]
    SimpleDialogueManager dialogueManager;

    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
        objectManager = FindObjectOfType<ObjectManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenRocker()
    {
        StartCoroutine(rockerAnim());
    }
    private IEnumerator rockerAnim()
    {
        OpenText();

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        anim.SetActive(true);

        yield return new WaitForSeconds(3f);

        bikkuri.SetActive(true);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        sceneManagement.OnClear();
    }
    private void OpenText()
    {

        objectManager.Ontext = true;
        gameManager.itemGet = false;
        textBox.SetActive(true);
        dialogueManager.StartDialogue2(dialogue); // ‰ï˜b‚ðŠJŽn
    }
}
