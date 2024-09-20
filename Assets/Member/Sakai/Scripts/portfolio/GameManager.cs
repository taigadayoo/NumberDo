using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public Image yesImage;
    public Image noImage;
    public Sprite pushYest;
    public Sprite pushNo;
    public Sprite nomalYes;
    public Sprite nomalNo;
    SceneManagement sceneManagement;

    ObjectManager objectManager;
    public bool? itemGet = false;
    public bool? itemGet2 = false;
    SampleSoundManager sampleSoundManager;
    // Start is called before the first frame update
    void Start()
    {
        sampleSoundManager = FindFirstObjectByType<SampleSoundManager>();
        sceneManagement = FindObjectOfType<SceneManagement>();
        objectManager = FindObjectOfType<ObjectManager>();
       
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            sceneManagement.OnGameOver();
        }
    }
    public void Yes()
    {
        objectManager.miniGameDead.SetActive(false);
        objectManager.miniGame.SetActive(true);
        yesImage.sprite = pushYest;

        StartCoroutine(RevertSpriteYes());
    }
    public void No()
    {
        objectManager.miniGameDead.SetActive(false);
        noImage.sprite = pushNo;
        StartCoroutine(RevertSpriteNo());
        objectManager.allColliderSwicth(true);
        SampleSoundManager.Instance.PlayBgm(BgmType.BGM3);
    }
    private IEnumerator RevertSpriteYes()
    {
        // 指定した秒数だけ待つ
        yield return new WaitForSeconds(0.2f);

        // スプライトをnomalImageに戻す
        yesImage.sprite = nomalYes;
    }
    private IEnumerator RevertSpriteNo()
    {
        // 指定した秒数だけ待つ
        yield return new WaitForSeconds(0.2f);

        // スプライトをnomalImageに戻す
        noImage.sprite = nomalNo;
    }
}
