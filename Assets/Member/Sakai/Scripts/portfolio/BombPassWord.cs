using UnityEngine;
using UnityEngine.UI;

public class BombPassword : MonoBehaviour
{
    [SerializeField]
    public InputField inputField; // パスワードのテキスト入力フィールド

    ObjectManager objectManager; // オブジェクトマネージャーの参照
    [SerializeField]
    ItemGetSet getSet; // アイテム取得/設定スクリプトの参照
    ItemBer itemBer; // アイテム管理スクリプトの参照
    [SerializeField]
    SceneManagement sceneManagement; // シーン管理スクリプトの参照
    private bool okBombPass = false; // パスワードが正解かどうかのフラグ
    SampleSoundManager sampleSoundManager; // サウンドマネージャーの参照

    public GameObject doorKey; // ドアのキーのオブジェクト
    Interactable interactable; // インタラクト可能なオブジェクトの参照

    public Text digit1; // 1桁目のテキスト
    public Text digit2; // 2桁目のテキスト
    public Text digit3; // 3桁目のテキスト
    public Text digit4; // 4桁目のテキスト

    Timer timer; // タイマーオブジェクトの参照
    // 各桁の現在の値
    private int[] digits = new int[4]; // 各桁の数字を保持する配列

    // 正しいパスワード
    private int[] correctPassword = new int[4] { 1, 9, 0, 2 };

    private void Start()
    {
        // 各オブジェクトの参照を取得
        timer = FindObjectOfType<Timer>();
        UpdateDigitTexts(); // 各桁のテキストを初期化
        objectManager = FindObjectOfType<ObjectManager>();
        itemBer = FindObjectOfType<ItemBer>();
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();

        // 初期化
        objectManager.OnePassWord = false;
        objectManager.Ontext = true;
    }

    private void Update()
    {
        // ゲームオブジェクトがアクティブな場合
        if (this.gameObject.activeSelf)
        {
            objectManager.Ontext = true; // テキストの表示を有効化
        }

        // パスワードが正しい場合の処理
        if (IsPasswordCorrect())
        {
            if (sampleSoundManager != null)
            {
                // サウンドを再生
                sampleSoundManager.PlaySe(SeType.SE9);
            }
            // アイテムの追加
            itemBer.AddItem(objectManager.items[16]);
            // 画像の変更
            getSet.ImageChange(22);
            // パスワード入力画面を非表示に
            objectManager.bombPass.SetActive(false);
            // 全てのコライダーを有効化
            objectManager.allColliderSwicth(true);
            // タイマーの停止
            timer.Stop();
            // ロック状態の解除
            objectManager.bombRock.SetActive(false);
            objectManager.bombUnrock.SetActive(true);
        }

        // 各桁のクリックをチェック
        CheckDigitClick(digit1, 0);
        CheckDigitClick(digit2, 1);
        CheckDigitClick(digit3, 2);
        CheckDigitClick(digit4, 3);
    }

    public void CheckPassword()
    {
        // 入力フィールドのテキストを取得（現時点では未使用）
        string inputPassword = inputField.text;
    }

    private void OkPass()
    {
        // アイテムの追加
        itemBer.AddItem(objectManager.items[3]);
        // 画像番号の設定
        objectManager.imageNum = 3;
        // 画像の変更
        getSet.ImageChange(objectManager.imageNum);

        // パスワードが正解であることを設定
        objectManager.OnePassWord = true;

        if (sampleSoundManager != null)
        {
            // サウンドを再生
            SampleSoundManager.Instance.PlaySe(SeType.SE4);
        }
    }

    void CheckDigitClick(Text digitText, int digitIndex)
    {
        // マウスボタンが押され、かつクリックが指定のテキストUI上で行われた場合
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverUIElement(digitText))
            {
                // 対応する桁の数字を1増加
                digits[digitIndex]++;

                // 10になったら0に戻す
                if (digits[digitIndex] > 9)
                {
                    digits[digitIndex] = 0;
                }

                // 更新された数字をテキストに表示
                UpdateDigitTexts();
            }
            // どの桁にもマウスがオーバーしていない場合の処理
            else if (!IsMouseOverUIElement(digit1) &&
                     !IsMouseOverUIElement(digit2) &&
                     !IsMouseOverUIElement(digit3) &&
                     !IsMouseOverUIElement(digit4))
            {
                objectManager.Ontext = false;
                objectManager.password.SetActive(false);
                objectManager.OnPass = false;
                objectManager.OnBox4 = false;
            }
        }
    }

    void UpdateDigitTexts()
    {
        // 各桁の数字をテキストに反映
        digit1.text = digits[0].ToString();
        digit2.text = digits[1].ToString();
        digit3.text = digits[2].ToString();
        digit4.text = digits[3].ToString();
    }

    bool IsMouseOverUIElement(Text textElement)
    {
        // UI要素の境界矩形を取得
        RectTransform rectTransform = textElement.GetComponent<RectTransform>();
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);

        // マウス位置がUI要素の範囲内にあるかをチェック
        return rectTransform.rect.Contains(localMousePosition);
    }

    bool IsPasswordCorrect()
    {
        // 入力された数字が正しいパスワードかをチェック
        for (int i = 0; i < digits.Length; i++)
        {
            if (digits[i] != correctPassword[i])
            {
                return false;
            }
        }
        return true;
    }
}