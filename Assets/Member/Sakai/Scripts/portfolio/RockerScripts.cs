using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RockerScripts : MonoBehaviour
{
    public GameObject anim; // アニメーション用のゲームオブジェクト
    [SerializeField]
    private VideoPlayer videoPlayer; // 動画再生用のVideoPlayer
    ObjectManager objectManager; // オブジェクト管理用
    SceneManagement sceneManagement; // シーン管理用
    public GameObject bikkuri; // 驚きの効果を持つゲームオブジェクト
    public GameObject textBox; // テキストボックスのゲームオブジェクト
    public GameObject blackBack; // 黒い背景用のゲームオブジェクト
    GameManager gameManager; // ゲーム全体の管理用
    [SerializeField]
    SimpleDialogueManager dialogueManager; // 会話管理用

    SampleSoundManager soundManager; // サウンド管理用
    public Dialogue dialogue; // 会話の内容

    // Start is called before the first frame update
    void Start()
    {
        // 各マネージャーのインスタンスを取得
        sceneManagement = FindObjectOfType<SceneManagement>();
        objectManager = FindObjectOfType<ObjectManager>();
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SampleSoundManager>();
    }

    // Update is called once per frame


    // ロッカーを開くメソッド
    public void OpenRocker()
    {
        StartCoroutine(rockerAnim()); // アニメーションを開始
    }

    // ロッカーアニメーションのコルーチン
    private IEnumerator rockerAnim()
    {
        OpenText(); // テキストを表示

        // クリックされるのを待つ
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        soundManager.StopBgm(); // 背景音楽を停止

        // もう一度クリックを待つ
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        blackBack.SetActive(true); // 黒い背景を表示
        soundManager.PlaySe(SeType.SE8); // 効果音を再生

        yield return new WaitForSeconds(1f); // 1秒待機

        anim.SetActive(true); // アニメーションを表示
        videoPlayer.Play(); // 動画を再生

        yield return new WaitForSeconds(4.5f); // 動画再生時間分待機

        bikkuri.SetActive(true); // 驚きのオブジェクトを表示

        yield return new WaitForSeconds(3f); // 3秒待機

        sceneManagement.OnMainGameMove(); // メインゲームに移動
    }

    // テキストを開くメソッド
    private void OpenText()
    {
        objectManager.Ontext = true; // テキスト表示を有効化
        gameManager.itemGet = false; // アイテム取得フラグを無効化
        textBox.SetActive(true); // テキストボックスを表示
        dialogueManager.StartDialogue2(dialogue); // 会話を開始
    }
}