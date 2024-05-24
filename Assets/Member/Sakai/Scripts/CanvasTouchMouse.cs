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

    // 特定のオブジェクトを識別するためのタグ
    public string keyTag = "Key";
    public string bombTag = "Bomb";
    private CheckBool lastClickedObject;
    SceneManagement sceneManagement;
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
                    // 前回クリックされたオブジェクトのisCheckをfalseに設定し、アルファ値を元に戻す
                    if (lastClickedObject != null && lastClickedObject != clickableObject)
                    {
                        lastClickedObject.isCheck = false;
                        SetAlpha(lastClickedObject.gameObject, 0.5f); // アルファ値を元に戻す
                    }

                    // 現在のオブジェクトのisCheckをtrueに設定し、アルファ値を半透明にする
                    clickableObject.isCheck = true;
                    SetAlpha(hitObject, 1f); // アルファ値を半透明に設定
                    SampleSoundManager.Instance.PlaySe(SeType.SE1);
                    // 現在のオブジェクトをlastClickedObjectとして記憶
                    lastClickedObject = clickableObject;
                    if (hitObject.CompareTag(keyTag))
                    {
                        isKeySelected = true;
                    }
                }


                break;
            }
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag(bombTag))
            {
                // ボムがマウスでクリックされた場合の処理
                if (isKeySelected)
                {
                    sceneManagement.OnClear();
                }
                isKeySelected = false;
            }
        }
    }
    private void SetAlpha(GameObject obj, float alpha)
    {
        Image image = obj.GetComponent<Image>();
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ドアがクリックされ、カギが選択されていて、ドアに触れた場合
        if (clickBomb != null && lastClickedObject != null && lastClickedObject.gameObject.CompareTag(keyTag) && other.gameObject == clickBomb)
        {
            // ドアに触れた際にシーンを変更する処理
            Debug.Log("Door unlocked with key! Changing scene...");
          
        }
    }

}
