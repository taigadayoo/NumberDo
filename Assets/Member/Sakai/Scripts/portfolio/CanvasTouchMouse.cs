using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasTouchMouse : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    public bool isCheck = false;

    private GameObject clickBomb;
    public bool isKeySelected = false;
    public bool isLightSelected = false;
    public bool isCandleSelected = false;
    public bool isKnifeSelected = false;
    public bool isNomalKnifeSelected = false;
    public bool isKeyDoorSelected = false;
    public bool isCandle2Selected = false;

    // 特定のオブジェクトを識別するためのタグ
    public string keyTag = "Key";
    public string bombTag = "Bomb";
    public string rockerTag = "Rocker";
    public string LightTag = "BlackLight";
    public string CandleFireTag = "candleFire";
    public string KnifeTag = "candleKnife";
    public string nomalKnifeTag = "knife";
    public string PictureTag = "Picture";
    public string keyDoorTag = "KeyDoor";
    public string CandleTag = "CandleFire2";
    public CheckBool lastClickedObject;
    public CheckBool previousClickedObject; // ひとつ前にクリックされたオブジェクト

    SceneManagement sceneManagement;
    ItemLight itemLight;
    ItemLight itemNextLight;
    [SerializeField]
    RockerScripts rockerScripts;

    // クリックされた最後の2つのオブジェクトを格納するリスト
    private List<CheckBool> clickedObjects = new List<CheckBool>();
    MixImageScripts imageScripts;

    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
        if (raycaster == null)
        {
            raycaster = GetComponent<GraphicRaycaster>();
        }
        if (eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }
        imageScripts = FindObjectOfType<MixImageScripts>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Input.mousePosition;

         
            // マウスのポインタ位置からRayを飛ばす
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            // すべてのUI要素を格納するリスト
            List<RaycastResult> results = new List<RaycastResult>();

            // Raycastを実行
            raycaster.Raycast(pointerEventData, results);

            // ヒットしたUI要素をチェック
            foreach (RaycastResult result in results)
            {
                GameObject hitObject = result.gameObject;
                CheckBool clickableObject = hitObject.GetComponent<CheckBool>();

                if (clickableObject != null)
                {
                    itemNextLight = clickableObject.GetComponent<ItemLight>();

                    // 同じオブジェクトが再度クリックされた場合
                    if (clickableObject == lastClickedObject)
                    {
                        imageScripts.foundMatch = false; // 一方の選択が外れた場合にfalseに設定

                        imageScripts.mixImage.enabled = false;
                        // 選択を解除
                        clickableObject.isCheck = false;
                        itemNextLight.ChangeNomal();
                        Debug.Log("Deselected: " + hitObject.name);

                        // 同じオブジェクトを三回クリックしてもpreviousClickedObjectがnullにならないようにする
                        if (previousClickedObject != null)
                        {
                            // 前のオブジェクトを再び選択状態にして光らせる
                            lastClickedObject = previousClickedObject;
                            itemLight = previousClickedObject.GetComponent<ItemLight>();
                            itemLight.ChangeLight();
                            previousClickedObject = null; // 一度リセット
                        }
                        else
                        {
                            lastClickedObject = null; // 選択を完全に解除
                        }
                    }
                    else
                    {
                        // 前のオブジェクトが選択されていた場合は選択解除
                        if (previousClickedObject != null && previousClickedObject != clickableObject)
                        {
                            imageScripts.foundMatch = false; // 一方の選択が外れた場合にfalseに設定
                            imageScripts.mixImage.enabled = false;
                            itemLight = previousClickedObject.GetComponent<ItemLight>();
                            previousClickedObject.isCheck = false;
                            itemLight.ChangeNomal();
                        }

                        // 現在のオブジェクトのisCheckをtrueに設定し、ChangeLightを呼び出す
                        clickableObject.isCheck = true;
                        itemNextLight.ChangeLight();

                        // lastClickedObjectをpreviousClickedObjectに移す
                        previousClickedObject = lastClickedObject;

                        // 現在のオブジェクトをlastClickedObjectにセット
                        lastClickedObject = clickableObject;

                        // clickedObjectsリストを更新
                        clickedObjects.Add(clickableObject);
                        if (clickedObjects.Count > 2)
                        {
                            clickedObjects.RemoveAt(0); // 2つまで保持
                        }
                    }

                    // タグに基づいてフラグを更新
                    isKeySelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(keyTag));
                    isLightSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(LightTag));
                    isCandleSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(CandleFireTag));
                    isKnifeSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(KnifeTag));
                    isKeyDoorSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(keyDoorTag));
                    isNomalKnifeSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(nomalKnifeTag));
                    isCandle2Selected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(CandleTag));
                }

                break; // 最初のヒットしたUI要素のみ処理
            }

            // シーン上のオブジェクトを検出
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag(rockerTag))
            {
                // ボムがマウスでクリックされた場合の処理
                if (isKeySelected)
                {
                    SceneManager.LoadScene("TutorialScenarioScene2");
                }
                isKeySelected = false;
            }
        }
    }
}