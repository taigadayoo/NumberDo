using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timeRemaining = 180f; // タイマーの初期値を3分に設定
    private bool isPaused = false;
    private bool OneGameOver = false;
    [SerializeField]
    SceneManagement sceneManagement;
    ObjectManager objectManager;
    GameManager gameManager;
    private void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
      gameManager =  FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (!isPaused && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // 経過時間を減算
            DisplayTime(timeRemaining); // 残り時間を表示
        }
        else if(timeRemaining <= 0 && !OneGameOver)
        {
            timeRemaining = 0; // タイマーが0になったら、時間を0に設定
            DisplayTime(timeRemaining); // 残り時間を表示
            SampleSoundManager.Instance.StopBgm();
            gameManager.isGameOver = true;
                OneGameOver = true;
            
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // 分と秒に変換
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // タイマーの表示形式を設定（00:00）
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
    public void Stop()
    {
        isPaused = true;
        objectManager.Ontext = false;
    }

    // タイマーを再開する関数
    public void Restart()
    {
        isPaused = false;
    }
}
