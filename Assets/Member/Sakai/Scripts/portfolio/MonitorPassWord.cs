using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorPassWord : MonoBehaviour
{
    // インスペクタで設定するInputFieldを参照
    [SerializeField]
    public InputField inputField;

    // 他のスクリプトの参照
    SampleSoundManager sampleSoundManager;
    ObjectManager objectManager;
    [SerializeField]
    ItemGetSet getSet;
    [SerializeField]
    SceneManagement sceneManagement;
    Interactable interactable;

    // パスワード確認の一度きりのフラグ
    private bool oneDeray = false;

    // 各桁を表すテキストオブジェクト
    public Text digit1;
    public Text digit2;
    public Text digit3;

    // 各桁の現在の値（アルファベット）を格納する配列
    private char[] chars = new char[3] { 'A', 'A', 'A' };

    // 正しいパスワード（アルファベット）を格納する配列
    private char[] correctPassword = new char[3] { 'S', 'A', 'J' };

    // 初期化処理
    private void Start()
    {
        // 各桁のテキストを更新
        UpdateDigitTexts();

        // 他のオブジェクトをシーンから取得
        objectManager = FindObjectOfType<ObjectManager>();
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
    }

    private void Update()
    {
        // パスワードが正しいか確認
        if (IsPasswordCorrect())
        {
            // パスワードが正しく、一度きりの処理がまだ行われていない場合
            if (!oneDeray)
            {
                // コルーチンを開始
                StartCoroutine(BombDeray());
                oneDeray = true;
            }
        }

        // 一度きりの処理がまだ行われていない場合にのみ桁のチェックを行う
        if (!oneDeray)
        {
            CheckDigitClick(digit1, 0);
            CheckDigitClick(digit2, 1);
            CheckDigitClick(digit3, 2);
        }
    }

    // コルーチン：パスワード正解時の遅延処理
    IEnumerator BombDeray()
    {
        // 正解のサウンドを再生
        sampleSoundManager.PlaySe(SeType.SE4);
        yield return new WaitForSeconds(1f);

        // 他のオブジェクトの状態を変更
        objectManager.allColliderSwicth(false);
        objectManager.monitorPass.SetActive(false);
        objectManager.miniGameZoom.SetActive(true);
        objectManager.zoomOffColMain.SetActive(false);
        objectManager.textEnd = false;
    }

    // パスワードのチェック（未使用）
    public void CheckPassword()
    {
        string inputPassword = inputField.text;
        // 現在は使用されていませんが、必要に応じて処理を追加できます
    }

    // 各桁のクリックを確認し、クリックされた場合に次のアルファベットに変更
    void CheckDigitClick(Text digitText, int digitIndex)
    {
        // 左クリックが押された場合
        if (Input.GetMouseButtonDown(0))
        {
            // クリックが指定のテキスト上で行われたか確認
            if (IsMouseOverUIElement(digitText))
            {
                // 対応する桁のアルファベットを次に進める
                chars[digitIndex] = NextCharacter(chars[digitIndex]);

                // 更新されたアルファベットをテキストに表示
                UpdateDigitTexts();
            }
        }
    }

    // 各桁のテキストを更新
    void UpdateDigitTexts()
    {
        digit1.text = chars[0].ToString();
        digit2.text = chars[1].ToString();
        digit3.text = chars[2].ToString();
    }

    // 次のアルファベットを返す ('Z' の次は 'A' に戻る)
    char NextCharacter(char current)
    {
        if (current == 'Z')
            return 'A';  // 'Z' の次は 'A' に戻る
        else
            return (char)(current + 1);
    }

    // 指定のUI要素上でマウスがクリックされたかを確認
    bool IsMouseOverUIElement(Text textElement)
    {
        // テキストのRectTransformを取得
        RectTransform rectTransform = textElement.GetComponent<RectTransform>();

        // マウスの座標をUI要素のローカル座標に変換
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);

        // UI要素の矩形内にマウス座標が含まれているかを確認
        return rectTransform.rect.Contains(localMousePosition);
    }

    // パスワードが正しいか確認
    bool IsPasswordCorrect()
    {
        for (int i = 0; i < chars.Length; i++)
        {
            // 各桁の文字が正しいか確認
            if (chars[i] != correctPassword[i])
            {
                return false;
            }
        }
        return true;
    }
}