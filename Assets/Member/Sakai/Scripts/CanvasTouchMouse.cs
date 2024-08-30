using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasTouchMouse : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    public bool isCheck = false;

    private GameObject clickBomb;
    private bool isKeySelected = false;
    public bool isLightSelected = false;
    public bool isCandleSelected = false;
    public bool isKnifeSelected = false;
    public bool isKeyDoorSelected = false;

    // 特定のオブジェクトを識別するためのタグ
    public string keyTag = "Key";
    public string bombTag = "Bomb";
    public string rockerTag = "Rocker";
    public string LightTag = "BlackLight";
    public string CandleFireTag = "candleFire";
    public string KnifeTag = "candleKnife";
    public string PictureTag = "Picture";
    public string keyDoorTag = "KeyDoor";
    public CheckBool lastClickedObject;
    public CheckBool previousClickedObject; // ひとつ前にクリックされたオブジェクト


    SceneManagement sceneManagement;
    ItemLight itemLight;
    ItemLight itemNextLight;
    [SerializeField]
    RockerScripts rockerScripts;
    // クリックされた最後の2つのオブジェクトを格納するリスト
    private List<CheckBool> clickedObjects = new List<CheckBool>();

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
                        // 選択を解除
                        clickableObject.isCheck = false;
                        itemNextLight.ChangeNomal();
                        Debug.Log("Deselected: " + hitObject.name);

                        // lastClickedObjectとpreviousClickedObjectをリセット
                        lastClickedObject = null;
                        if (previousClickedObject == clickableObject)
                        {
                            previousClickedObject = null;
                        }
                        else if (previousClickedObject != null)
                        {
                            // 一つ前のオブジェクトは選択されたまま
                            lastClickedObject = previousClickedObject;
                            itemLight = previousClickedObject.GetComponent<ItemLight>();
                            itemLight.ChangeLight();
                        }
                    }
                    else
                    {
                        // 前にクリックされたオブジェクトの処理
                        if (previousClickedObject != null)
                        {
                            itemLight = previousClickedObject.GetComponent<ItemLight>();
                            previousClickedObject.isCheck = false;
                            itemLight.ChangeNomal();
                        }

                        // 現在のオブジェクトのisCheckをtrueに設定し、ChangeLightを呼び出してスプライトを入れ替える
                        clickableObject.isCheck = true;
                        itemNextLight.ChangeLight();
                        //SampleSoundManager.Instance.PlaySe(SeType.SE1);

                        // lastClickedObjectをpreviousClickedObjectに移す
                        previousClickedObject = lastClickedObject;

                        // 現在のオブジェクトをlastClickedObjectとして記憶
                        lastClickedObject = clickableObject;

                        // クリックされた最後の2つのオブジェクトを管理
                        clickedObjects.Add(clickableObject);
                        if (clickedObjects.Count > 2)
                        {
                            clickedObjects.RemoveAt(0);
                        }
                    }
                    isKeySelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(keyTag));
                    isLightSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(LightTag));
                    isCandleSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(CandleFireTag));
                    isKnifeSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(KnifeTag));
                    isKeyDoorSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(keyDoorTag));


                
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
                    rockerScripts.OpenRocker();
                }
                isKeySelected = false;
            }
            //if (hit.collider != null && hit.collider.CompareTag(PictureTag))
            //{
            //    // ボムがマウスでクリックされた場合の処理
            //    if (isLightSelected)
            //    {
            //        Debug.Log("これはライトです。");
            //    }
            //    isLightSelected = false;
            //}
        }
    }
}