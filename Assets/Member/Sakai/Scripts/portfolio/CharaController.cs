using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharaController : MonoBehaviour
{
    //Xの上限
    float xLimit = 320f;
    public bool isDead = false;

    SampleSoundManager soundManager;
    [SerializeField]
    SceneManagement sceneManagement;

    [SerializeField]
    Ballgenerator ballgenerator;
    // RectTransformの参照
    private RectTransform rectTransform;
    public Image rightImage;
    public Image leftImage;
    public Sprite pushRight;
    public Sprite pushLeft;
    public Sprite nomalRight;
    public Sprite nomalLeft;
    ObjectManager objectManager;
    void Start()
    {
        // RectTransformコンポーネントを取得
        rectTransform = GetComponent<RectTransform>();
        soundManager = FindObjectOfType<SampleSoundManager>();
        objectManager = FindObjectOfType<ObjectManager>();
    }
    private void OnEnable()
    {
        leftImage.sprite = nomalLeft;
        rightImage.sprite = nomalRight;
    }
    public void RbuttonClick()
    {
        SampleSoundManager.Instance.PlaySe(SeType.SE11);
        // 位置を変更
        rectTransform.anchoredPosition += new Vector2(160, 0);
        if (soundManager != null)
        {
            SampleSoundManager.Instance.PlaySe(SeType.SE3);
        }
        rightImage.sprite = pushRight;

        StartCoroutine(RevertSpriteRight());
    }

    public void LButtonClick()
    {
        // 位置を変更
        SampleSoundManager.Instance.PlaySe(SeType.SE11);
        rectTransform.anchoredPosition += new Vector2(-160, 0);
        if (soundManager != null)
        {
            SampleSoundManager.Instance.PlaySe(SeType.SE3);
        }
        leftImage.sprite = pushLeft;

        StartCoroutine(RevertSpriteLeft());
    }
    private IEnumerator RevertSpriteRight()
    {
        // 指定した秒数だけ待つ
        yield return new WaitForSeconds(0.2f);

        // スプライトをnomalImageに戻す
        rightImage.sprite = nomalRight;
    }
    private IEnumerator RevertSpriteLeft()
    {
        // 指定した秒数だけ待つ
        yield return new WaitForSeconds(0.2f);

        // スプライトをnomalImageに戻す
        leftImage.sprite = nomalLeft;
    }

    void Update()
    {
        // 現在の位置を取得
        Vector2 currentPos = rectTransform.anchoredPosition;

        // Mathf.ClampでXの値を範囲内に収める
        currentPos.x = Mathf.Clamp(currentPos.x, -xLimit, xLimit);

        // 位置を更新
        rectTransform.anchoredPosition = currentPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ballprefab")) // 弾と衝突したらプレイヤーを消滅させる
        {
            //GameOverSceneを呼び出す
            isDead = true;
            ballgenerator.ClearAllBalls();
            objectManager.OnMiniGame = false;
            objectManager.miniGameDead.SetActive(true);
            objectManager.miniGame.SetActive(false);
            SampleSoundManager.Instance.PlaySe(SeType.SE12);
         
            ////ゲーム内の時間を止める
            //Time.timeScale = 0f;          
        }
    }
}
