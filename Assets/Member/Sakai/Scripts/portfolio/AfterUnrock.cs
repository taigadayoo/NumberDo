using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterUnrock : MonoBehaviour
{
    // ObjectManagerスクリプトを参照
    [SerializeField]
    ObjectManager objectManager;

    // SimpleDialogueManagerスクリプトを参照
    [SerializeField]
    SimpleDialogueManager dialogueManager;

    // GameManagerスクリプトを参照
    [SerializeField]
    GameManager gameManager;

    // ダイアログの情報を保持
    public Dialogue dialogue;

    // テキストボックスのゲームオブジェクト
    public GameObject textBox;

    // 一度だけUnrocking処理を実行するためのフラグ
    private bool oneUnrock = false;

    // Startメソッドは初期化時に呼び出される
    void Start()
    {
        // 特に初期化処理は不要なので、空のままにしている
    }

    // Updateメソッドは毎フレーム呼び出される
    void Update()
    {
        // unrockingがtrueで、oneUnrockがfalseのときUnrockingメソッドを実行
        if (objectManager.unrocking && !oneUnrock)
        {
            Unrocking();
            oneUnrock = true; // 一度実行したことを記録
        }
    }

    // Unrockingの処理を実行するメソッド
    private void Unrocking()
    {
        // テキストが表示中であることを示すフラグをオンにする
        objectManager.Ontext = true;

        // 対象のオブジェクトをアクティブにする
        objectManager.unrockingRock.SetActive(true);

        // アイテムを取得済みの状態をリセット
        gameManager.itemGet = false;

        // テキストボックスを表示
        textBox.SetActive(true);

        // ダイアログを開始
        dialogueManager.StartDialogue2(dialogue);
    }
}