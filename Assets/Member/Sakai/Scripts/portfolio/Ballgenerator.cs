using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ballgenerator : MonoBehaviour
{
    Timer timer;
    public RectTransform[] positions; // ランダムに選択される位置の配列
    public GameObject BallPrefab; // 生成するボールのプレハブ
    private float delta = 0;
    public GameObject Balls;
    public float time = 1.3f; // インターバル時間
    public TimeCounter timeCounter; // カウントダウンを管理するスクリプト
    public Text countdownText; // UI上でカウントダウンを表示するText
    public int countdownTime = 3; // カウントダウンの開始値
    private bool isFirstEnable = true;
    private List<GameObject> ballList = new List<GameObject>();
    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        SampleSoundManager.Instance.StopBgm();
        StartCoroutine(StartCountdown());
       
    }
    private void OnEnable()
    {

            if (isFirstEnable)
            {
                // 最初の有効化ではOnEnableは何もしない
                isFirstEnable = false;
            }
            else
            {
            SampleSoundManager.Instance.StopBgm();

            StartCoroutine(StartCountdown());
            }
          
    }
    void Update()
    {
        StartCoroutine(MiniGameStart());
       
    }
    private IEnumerator StartCountdown()
    {
        SampleSoundManager.Instance.PlayBgm(BgmType.BGM4);

        SampleSoundManager.Instance.PlaySe(SeType.SE10);
        int currentTime = countdownTime;

        while (currentTime > 0)
        {
            // 現在のカウントを表示
            countdownText.text = currentTime.ToString();

            // 1秒待つ
            yield return new WaitForSeconds(1f);

            // カウントを減らす
            currentTime--;
        }

  

        countdownText.text = "";
    }
    private IEnumerator MiniGameStart()
    {
        timer.Stop();
        yield return new WaitForSeconds(3);
      
        this.delta += Time.deltaTime;

        // インターバル時間を超えた場合
        if (this.delta > this.time)
        {
            // カウントダウンが1以上の場合
            if (1 <= timeCounter.countdown)
            {
                // 時間をリセット
                this.delta = 0;

                // ランダムに位置を選択
                int randomIndex = Random.Range(0, positions.Length);
                Vector2 randomPosition = positions[randomIndex].anchoredPosition;

                // ボールのインスタンスを生成
                GameObject ball = Instantiate(BallPrefab, Balls.transform);
                RectTransform ballRectTransform = ball.GetComponent<RectTransform>();

                // 生成したボールの位置を設定
                ballRectTransform.anchoredPosition = randomPosition;
                ballList.Add(ball);
            }
        }
    }
    public void ClearAllBalls()
    {
        foreach (GameObject ball in ballList)
        {
            Destroy(ball);
        }

        // リストをクリア
        ballList.Clear();
    }
}

