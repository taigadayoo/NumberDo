using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BombPass : MonoBehaviour
{
    [SerializeField]
    public InputField inputField; // パスワードを入力するためのフィールド
    [SerializeField]
    public Canvas targetCanvas;  // ターゲットキャンバスをInspectorから設定する

    ObjectManager objectManager; // ObjectManagerスクリプトの参照
    [SerializeField]
    ItemGetSet getSet; // ItemGetSetスクリプトの参照
    ItemBer itemBer; // ItemBerスクリプトの参照
    [SerializeField]
    SceneManagement sceneManagement; // シーン管理の参照
    private bool okBombPass = false; // パスワードが正解であるかを示すフラグ
    SampleSoundManager sampleSoundManager; // サウンドマネージャーの参照
    [SerializeField]
    HintTextChange hint; // ヒントテキスト変更のための参照
    public GameObject doorKey; // ドアの鍵
    Interactable interactable; // インタラクションのためのスクリプト参照

    public bool oneDeray = false; // 一度だけ遅延処理を行うためのフラグ
    public Text digit1; // 1桁目のテキスト
    public Text digit2; // 2桁目のテキスト
    public Text digit3; // 3桁目のテキスト
    public Text digit4; // 4桁目のテキスト

    Timer timer; // タイマーの参照

    // 各桁の現在の値
    private int[] digits = new int[4];

    // 正しいパスワード
    private int[] correctPassword = new int[4] { 1, 9, 0, 2 };

    private void Start()
    {
        // タイマーと他のオブジェクトを取得
        timer = FindObjectOfType<Timer>();
        UpdateDigitTexts();
        objectManager = FindObjectOfType<ObjectManager>();
        itemBer = FindObjectOfType<ItemBer>();
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();

        // 初期設定
        objectManager.OnePassWord = false;
        objectManager.Ontext = true;
    }

    private void Update()
    {
        // オブジェクトがアクティブな場合、テキスト表示フラグをオンにする
        if (this.gameObject.activeSelf)
        {
            objectManager.Ontext = true;
        }

        // パスワードが正しいかを確認
        if (IsPasswordCorrect())
        {
            if (!oneDeray)
            {
                // タイマーを停止し、遅延処理を開始
                timer.Stop();
                StartCoroutine(BombDeray());
                oneDeray = true;
            }
        }

        // 遅延処理が行われていない場合のみ桁のチェックを行う
        if (!oneDeray)
        {
            CheckDigitClick(digit1, 0);
            CheckDigitClick(digit2, 1);
            CheckDigitClick(digit3, 2);
            CheckDigitClick(digit4, 3);
        }
    }

    public void CheckPassword()
    {
        string inputPassword = inputField.text;
        // 入力フィールドからパスワードを取得するためのメソッド (未使用)
    }

    IEnumerator BombDeray()
    {
        // サウンドを再生し、1秒遅延後に処理を行う
        if (sampleSoundManager != null)
        {
            sampleSoundManager.PlaySe(SeType.SE4);
        }
        yield return new WaitForSeconds(1f);

        // 各種設定を変更し、オブジェクトをアクティブ/非アクティブにする
        objectManager.textEnd = true;
        itemBer.AddItem(objectManager.items[16]);
        getSet.ImageChange(22);
        objectManager.allColliderSwicth(true);
        objectManager.unrock = true;
        objectManager.bombPass.SetActive(false);
        objectManager.zoomOffColMain.SetActive(false);
        objectManager.bombRock.SetActive(false);
        objectManager.bombUnrock.SetActive(true);
        hint.ImageChange(5);
    }

    private void OkPass()
    {
        // パスワードが正しかった場合の処理
        itemBer.AddItem(objectManager.items[3]);
        objectManager.imageNum = 3;
        getSet.ImageChange(objectManager.imageNum);

        objectManager.OnePassWord = true;

        if (sampleSoundManager != null)
        {
            SampleSoundManager.Instance.PlaySe(SeType.SE4);
        }
    }

    bool IsMouseOverTaggedUIElementInCanvas(string tag, Canvas targetCanvas)
    {
        // マウスの位置情報を取得
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        // 指定したキャンバスのグラフィックレイキャスターを使用してレイキャスト
        GraphicRaycaster raycaster = targetCanvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        // レイキャストの結果を確認
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit UI Element: " + result.gameObject.name + " with Tag: " + result.gameObject.tag);

            if (result.gameObject.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }

    void CheckDigitClick(Text digitText, int digitIndex)
    {
        // マウスボタンが押された際の処理
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverUIElement(digitText))
            {
                // 対応する桁のカウントを増加
                digits[digitIndex]++;

                // カウントが10に達したら0に戻す
                if (digits[digitIndex] > 9)
                {
                    digits[digitIndex] = 0;
                }

                // 更新されたカウントをテキストに表示
                UpdateDigitTexts();
            }
            // 他のUI要素がクリックされた場合の処理
            else if (!IsMouseOverUIElement(digit1) &&
                     !IsMouseOverUIElement(digit2) &&
                     !IsMouseOverUIElement(digit3) &&
                     !IsMouseOverUIElement(digit4))
            {
                if (!IsMouseOverTaggedUIElementInCanvas("IgnoreHide", targetCanvas))
                {
                    // 全てのコライダーを有効にして、テキスト表示フラグをオフにする
                    objectManager.allColliderSwicth(true);
                    objectManager.Ontext = false;
                    objectManager.bombPass.SetActive(false);
                    objectManager.OnPass = false;
                    objectManager.OnBox4 = false;
                    objectManager.textEnd = true;
                }
            }
        }
    }

    void UpdateDigitTexts()
    {
        // 各桁のカウントをテキストに反映
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
        // 入力されたカウンターの値がパスワードと一致するかをチェック
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