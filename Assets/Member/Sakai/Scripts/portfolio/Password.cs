using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Password : MonoBehaviour
{
    [SerializeField]
    public InputField inputField; // 入力フィールド

    ObjectManager objectManager; // オブジェクト管理用
    [SerializeField]
    ItemGetSet getSet; // アイテム取得・設定用
    ItemBer itemBer; // アイテム保持用
    [SerializeField]
    SceneManagement sceneManagement; // シーン管理用
    private bool okPass = false; // パスワードが正しいかどうか
    SampleSoundManager sampleSoundManager; // サウンド管理用
    public GameObject noOpenShelf; // 開いていない棚のゲームオブジェクト
    private Image shelfSprite; // 棚のスプライト
    public Sprite opendSprite; // 開いた状態のスプライト
    public GameObject zoomKey; // ズームキーのゲームオブジェクト
    public GameObject medicineShelf; // 薬棚のゲームオブジェクト
    Interactable interactable; // インタラクト可能オブジェクト

    public Text digit1; // 桁1のテキスト
    public Text digit2; // 桁2のテキスト
    public Text digit3; // 桁3のテキスト
    public Text digit4; // 桁4のテキスト
    public Canvas targetCanvas; // 対象キャンバス
    // 各桁の現在の値
    private int[] digits = new int[4];

    // 正しいパスワード
    private int[] correctPassword = new int[4] { 1, 9, 9, 6 };
    private bool oneDeray = false; // 一度だけ解除処理を行うためのフラグ

    private void Start()
    {
        UpdateDigitTexts(); // 桁のテキストを初期化
        objectManager = FindObjectOfType<ObjectManager>(); // オブジェクトマネージャーの取得
        itemBer = FindObjectOfType<ItemBer>(); // アイテム保持オブジェクトの取得
        sampleSoundManager = FindObjectOfType<SampleSoundManager>(); // サウンドマネージャーの取得
        objectManager.OnePassWord = false; // パスワード解除フラグを初期化
        objectManager.Ontext = true; // テキスト表示フラグを初期化
        interactable = medicineShelf.GetComponent<Interactable>(); // 薬棚のインタラクトコンポーネントを取得
        shelfSprite = noOpenShelf.GetComponent<Image>(); // 開いていない棚のスプライトを取得
    }

    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            objectManager.Ontext = true; // このオブジェクトがアクティブなとき、テキスト表示を有効に
        }

        // パスワードが正しい場合の処理
        if (IsPasswordCorrect())
        {
            if (!oneDeray) // 一度だけ解除処理を行う
            {
                StartCoroutine(BombDeray()); // 爆弾解除処理を開始
                oneDeray = true; // フラグを立てる
            }
        }

        if (!oneDeray) // まだ解除処理が行われていない場合
        {
            // 各桁のクリック処理をチェック
            CheckDigitClick(digit1, 0);
            CheckDigitClick(digit2, 1);
            CheckDigitClick(digit3, 2);
            CheckDigitClick(digit4, 3);
        }
    }

    public void CheckPassword()
    {
        string inputPassword = inputField.text; // 入力されたパスワードを取得
        // （パスワードの確認処理は未実装）
    }

    IEnumerator BombDeray()
    {
        sampleSoundManager.PlaySe(SeType.SE4); // 爆弾解除音を再生
        yield return new WaitForSeconds(1f); // 1秒待機
        sampleSoundManager.PlaySe(SeType.SE9); // 爆弾解除後の音を再生
        objectManager.unrocking = true; // オブジェクトの解除フラグを立てる
        objectManager.password.SetActive(false); // パスワード入力UIを非表示
        interactable.touchAction = Interactable.TouchAction.itemGeted; // アイテム取得アクションに変更
        shelfSprite.sprite = opendSprite; // 開いた棚のスプライトに変更
        zoomKey.SetActive(false); // ズームキーを非表示
        objectManager.zoomTutorial = false; // ズームチュートリアルを無効化
        objectManager.OnePassWord = true; // パスワード解除フラグを立てる
    }

    private void OkPass()
    {
        itemBer.AddItem(objectManager.items[3]); // アイテムを追加
        objectManager.imageNum = 3; // 画像番号を設定
        getSet.ImageChange(objectManager.imageNum); // 画像を変更

        objectManager.OnePassWord = true; // パスワード解除フラグを立てる

        if (sampleSoundManager != null)
        {
            SampleSoundManager.Instance.PlaySe(SeType.SE4); // 成功音を再生
        }
    }

    void CheckDigitClick(Text digitText, int digitIndex)
    {
        // マウスボタンが押され、クリックがUI要素上で行われた場合
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverUIElement(digitText)) // マウスが桁のUI要素上にある場合
            {
                // 対応する桁のカウントを増やす
                digits[digitIndex]++;

                // カウントが10になったら0に戻す
                if (digits[digitIndex] > 9)
                {
                    digits[digitIndex] = 0; // 0にリセット
                }

                // 更新されたカウントをテキストに表示
                UpdateDigitTexts();
            }
            else if (!IsMouseOverUIElement(digit1) &&
                     !IsMouseOverUIElement(digit2) &&
                     !IsMouseOverUIElement(digit3) &&
                     !IsMouseOverUIElement(digit4)) // どの桁にもマウスがない場合
            {
                if (!IsMouseOverTaggedUIElementInCanvas("IgnoreHide", targetCanvas)) // 特定のタグを持つUI要素でない場合
                {
                    objectManager.Ontext = false; // テキスト表示を無効化
                    objectManager.password.SetActive(false); // パスワード入力UIを非表示
                    objectManager.OnPass = false; // パスワードが正しくないフラグを無効化
                    objectManager.OnBox4 = false; // ボックス4のフラグを無効化
                    objectManager.colDeley = true; // コリジョンの遅延を有効化
                    objectManager.textEnd = true; // テキストの終了フラグを有効化
                    objectManager.zoomTutorial = false; // ズームチュートリアルを無効化
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

    bool IsMouseOverTaggedUIElementInCanvas(string tag, Canvas targetCanvas)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        // ターゲットのキャンバスのGraphicRaycasterを使ってRaycast
        GraphicRaycaster raycaster = targetCanvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results); // Raycastを実行

        // 結果をチェック
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit UI Element: " + result.gameObject.name + " with Tag: " + result.gameObject.tag);

            if (result.gameObject.CompareTag(tag)) // 特定のタグを持つ要素があるか確認
            {
                return true; // タグに一致する要素があればtrue
            }
        }

        return false; // 一致する要素がなければfalse
    }

    bool IsMouseOverUIElement(Text textElement)
    {
        // UI要素の境界矩形を取得
        RectTransform rectTransform = textElement.GetComponent<RectTransform>();
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);

        // 矩形内にマウスがあるか確認
        return rectTransform.rect.Contains(localMousePosition);
    }

    // 正しいパスワードか確認するメソッド
    bool IsPasswordCorrect()
    {
        // 各桁の値を確認
        for (int i = 0; i < correctPassword.Length; i++)
        {
            if (digits[i] != correctPassword[i]) // 一つでも一致しなければfalse
            {
                return false; // パスワードが間違っている
            }
        }
        return true; // 全桁一致すればtrue
    }
}