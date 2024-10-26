using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ゲームオーバー状態を管理するフラグ
    public bool isGameOver = false;

    // YesボタンとNoボタンのImageコンポーネント
    public Image yesImage;
    public Image noImage;

    // YesボタンとNoボタンが押されたときのスプライト
    public Sprite pushYest;
    public Sprite pushNo;

    // 通常時のYesボタンとNoボタンのスプライト
    public Sprite nomalYes;
    public Sprite nomalNo;

    // シーン管理用のオブジェクト
    SceneManagement sceneManagement;

    // オブジェクト管理用のオブジェクト
    ObjectManager objectManager;

    // アイテム取得状況を管理するフラグ
    public bool? itemGet = false;
    public bool? itemGet2 = false;

    // サウンド管理用のオブジェクト
    SampleSoundManager sampleSoundManager;

    // タイマー管理用のオブジェクト
    [SerializeField]
    Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        // SampleSoundManagerのインスタンスを取得
        sampleSoundManager = FindFirstObjectByType<SampleSoundManager>();

        // SceneManagementのインスタンスを取得
        sceneManagement = FindObjectOfType<SceneManagement>();

        // ObjectManagerのインスタンスを取得
        objectManager = FindObjectOfType<ObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームオーバー状態の場合、シーンのゲームオーバー処理を実行
        if (isGameOver)
        {
            sceneManagement.OnGameOver();
        }
    }

    // Yesボタンが押されたときの処理
    public void Yes()
    {
        // ミニゲームのデッド状態を非表示にし、ミニゲームを表示
        objectManager.miniGameDead.SetActive(false);
        objectManager.miniGame.SetActive(true);

        // Yesボタンのスプライトを変更
        yesImage.sprite = pushYest;

        // すべてのコライダーを無効にする
        objectManager.allColliderSwicth(false);

        // テキストの終了フラグを無効にする
        objectManager.textEnd = false;

        // スプライトを元に戻すコルーチンを開始
        StartCoroutine(RevertSpriteYes());
    }

    // Noボタンが押されたときの処理
    public void No()
    {
        // 現在のBGMを停止
        SampleSoundManager.Instance.StopBgm();

        // 新しいBGMを再生
        SampleSoundManager.Instance.PlayBgm(BgmType.BGM3);

        // テキストの終了フラグを有効にする
        objectManager.textEnd = true;

        // ミニゲームのデッド状態を非表示にする
        objectManager.miniGameDead.SetActive(false);

        // Noボタンのスプライトを変更
        noImage.sprite = pushNo;

        // スプライトを元に戻すコルーチンを開始
        StartCoroutine(RevertSpriteNo());

        // コライダーの遅延フラグを有効にする
        objectManager.colDeley = true;

        // タイマーをリスタート
        timer.Restart();
    }

    // Yesボタンのスプライトを元に戻すコルーチン
    private IEnumerator RevertSpriteYes()
    {
        // 指定した秒数だけ待つ
        yield return new WaitForSeconds(0.2f);

        // スプライトを通常のYesボタンに戻す
        yesImage.sprite = nomalYes;
    }

    // Noボタンのスプライトを元に戻すコルーチン
    private IEnumerator RevertSpriteNo()
    {
        // 指定した秒数だけ待つ
        yield return new WaitForSeconds(0.2f);

        // スプライトを通常のNoボタンに戻す
        noImage.sprite = nomalNo;
    }
}