using UnityEngine;

public class Interactable : MonoBehaviour
{
    // このオブジェクトに関連する会話データ
    public Dialogue dialogue;

    // 会話管理用のSimpleDialogueManager（Inspectorから設定も可能）
    [SerializeField]
    SimpleDialogueManager dialogueManager;

    // テキストボックスのゲームオブジェクト
    public GameObject textBox;

    // 各種管理オブジェクトの参照
    ObjectManager objectManager;
    GameManager gameManager;
    Timer timer;

    // タッチアクションの列挙型
    public enum TouchAction
    {
        noItem,   // アイテム未取得状態
        itemGet,  // アイテム取得状態
        itemGeted,// 取得済みアイテム状態
        fruit     // 特定のアイテム（フルーツ）状態
    }

    // タッチアクションの現在の状態
    [SerializeField]
    public TouchAction touchAction;

    // Startメソッドは、オブジェクトの初期化時に一度だけ呼ばれる
    void Start()
    {
        // タイマー、ゲーム管理、オブジェクト管理の各インスタンスを取得
        timer = FindObjectOfType<Timer>();
        gameManager = FindObjectOfType<GameManager>();
        if (dialogueManager == null)
        {
            // dialogueManagerが未設定の場合、自動で取得
            dialogueManager = FindObjectOfType<SimpleDialogueManager>();
        }
        objectManager = FindObjectOfType<ObjectManager>();
    }

    // Updateメソッドは、フレームごとに呼ばれる（今回は空）
    private void Update()
    {
    }

    // マウスがクリックされたときの処理
    void OnMouseDown()
    {
        // アイテムが取得されていない場合のみ処理を進める
        if (!objectManager.ItemGet)
        {
            objectManager.textEnd = false;

            // タッチアクションがitemGetの場合、会話を開始し、タッチアクションを更新
            if (dialogueManager != null && dialogue != null && touchAction == TouchAction.itemGet && !objectManager.Ontext)
            {
                // アイテム取得状態を更新
                gameManager.itemGet = true;
                gameManager.itemGet2 = true;

                // テキストボックスを表示して、会話を開始
                textBox.SetActive(true);
                dialogueManager.StartDialogue(dialogue);

                // タッチアクションを更新
                touchAction = TouchAction.itemGeted;
            }

            // タッチアクションがitemGetedの場合、取得済みの会話を開始
            if (dialogueManager != null && dialogue != null && touchAction == TouchAction.itemGeted && !objectManager.Ontext)
            {
                // テキストボックスを表示して、取得済みの会話を開始
                textBox.SetActive(true);
                dialogueManager.StartDialogueItemGeted(dialogue);

                // アイテム取得状態を更新
                gameManager.itemGet = true;
                gameManager.itemGet2 = false;
            }

            // タッチアクションがfruitの場合、フルーツの会話を開始し、タッチアクションを更新
            if (dialogueManager != null && dialogue != null && touchAction == TouchAction.fruit && !objectManager.Ontext)
            {
                // テキストボックスを表示して、フルーツの会話を開始
                textBox.SetActive(true);
                dialogueManager.StartDialogueFruit(dialogue);

                // アイテム取得状態を更新
                gameManager.itemGet = false;
                gameManager.itemGet2 = true;

                // タッチアクションを更新
                touchAction = TouchAction.itemGeted;
            }

            // タッチアクションがnoItemの場合、通常の会話を開始
            if (dialogueManager != null && dialogue != null && touchAction == TouchAction.noItem && !objectManager.Ontext)
            {
                // タグが "Monitor" であり、特定の条件が満たされている場合（コードが未記入の部分）
                if (gameObject.tag == "Monitor" && objectManager.OnKeyCode)
                {
                    // Monitorタグの処理（具体的な動作は不明）
                }
                else
                {
                    // 通常の会話を開始
                    gameManager.itemGet = false;
                    gameManager.itemGet2 = false;
                    textBox.SetActive(true);
                    dialogueManager.StartDialogue2(dialogue);

                    // タグが "MiniGame" の場合、ミニゲーム関連のフラグを更新
                    if (gameObject.tag == "MiniGame")
                    {
                        objectManager.OnMiniGame = true;
                        objectManager.OnKeyCode = true;
                    }
                }
            }
        }
    }
}