using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    // カウントダウン時間
    public float countdown = 13f;
    public bool isclier = false; // ゲームクリアフラグ
    public GameObject popup; // ポップアップオブジェクト
    public GameObject prefab; // プレハブオブジェクト
    public GameObject buttons; // ボタンオブジェクト
    public GameObject player; // プレイヤーオブジェクト
    ObjectManager objectManager; // ObjectManagerの参照
    Timer timer; // Timerの参照
    SampleSoundManager soundManager; // サウンドマネージャーの参照

    private bool OneClear = false; // 一度クリアしたかどうかのフラグ
    public Sprite clierImage; // クリア時の画像
    [SerializeField]
    Image spriteRenderer; // スプライトを表示するイメージコンポーネント

    private void Start()
    {
        // 各コンポーネントの初期化
        timer = FindFirstObjectByType<Timer>();
        objectManager = FindObjectOfType<ObjectManager>();
        soundManager = FindObjectOfType<SampleSoundManager>();

        // ObjectManagerが存在する場合、すべてのコライダーを無効にする
        if (objectManager != null)
        {
            objectManager.allColliderSwicth(false);
        }
    }

    void OnEnable()
    {
        // タイマーをリセット
        countdown = 13f;
    }

    // Updateはフレームごとに呼ばれる
    void Update()
    {
        // カウントダウンを進める
        countdown -= Time.deltaTime;

        // countdownが0以下になった場合の処理
        if (countdown <= 0)
        {
            // ボタンを無効にする
            buttons.SetActive(false);
            isclier = true; // クリアフラグを立てる
            player.SetActive(false); // プレイヤーを無効にする
            spriteRenderer.sprite = clierImage; // スプライトをクリア画像に変更
            objectManager.OnMiniGame = false; // ミニゲームのフラグを無効にする
            transform.parent.gameObject.SetActive(false); // 親オブジェクトを非表示にする

            // 一度もクリアしていない場合の処理
            if (!OneClear)
            {
                objectManager.miniGameClear.SetActive(true); // ミニゲームクリアUIを表示
                SampleSoundManager.Instance.StopBgm(); // 背景音楽を停止
                SampleSoundManager.Instance.PlaySe(SeType.SE13); // サウンドエフェクトを再生
                OneClear = true; // クリアフラグを立てる
            }

            // モニターの表示状態を切り替え
            objectManager.monitor.SetActive(false);
            objectManager.monitorGamed.SetActive(true);
            objectManager.colDeley = true; // コライダーの遅延を有効にする
            objectManager.OnKeyCode = false; // キーコードの処理を無効にする

            // サウンドマネージャーが存在する場合、別のサウンドエフェクトを再生
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE5);
            }
        }
    }
}