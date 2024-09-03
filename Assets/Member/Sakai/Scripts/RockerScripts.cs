using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RockerScripts : MonoBehaviour
{
    public GameObject anim;
    [SerializeField]
    private VideoPlayer videoPlayer;
    ObjectManager objectManager;
    SceneManagement sceneManagement;
    public GameObject bikkuri;
    public GameObject textBox;
    public GameObject blackBack;
    GameManager gameManager;
    [SerializeField]
    SimpleDialogueManager dialogueManager;

    SampleSoundManager soundManager;
    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
        objectManager = FindObjectOfType<ObjectManager>();
        gameManager = FindObjectOfType<GameManager>();
       soundManager =  FindObjectOfType<SampleSoundManager>();
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

        soundManager.StopBgm();

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

       
        blackBack.SetActive(true);
        soundManager.PlaySe(SeType.SE8);
        

        yield return new WaitForSeconds(1f);

        anim.SetActive(true);
        videoPlayer.Play();

        yield return new WaitForSeconds(4.5f);

        bikkuri.SetActive(true);

        yield return new WaitForSeconds(3f);

        sceneManagement.OnMainGameMove();
    }
    private void OpenText()
    {

        objectManager.Ontext = true;
        gameManager.itemGet = false;
        textBox.SetActive(true);
        dialogueManager.StartDialogue2(dialogue); // ‰ï˜b‚ðŠJŽn
    }
}
