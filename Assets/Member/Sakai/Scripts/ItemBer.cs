using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBer : MonoBehaviour
{
    public Slider slider;
    public List<GameObject> getItemList = new List<GameObject>();
    public List<GameObject> pageList = new List<GameObject>();
    public GameObject handle;
    public GameObject button;
    private int itemCount = 0;
    public bool OnBar = false;
    public bool ischak = false;
    [SerializeField]
    public Image image;
    [SerializeField]
    GameObject itemGetPanel;

    public GameObject Pages;
    public GameObject page;
    public Sprite OnBerImage;
    public Sprite OffBerImage;
    public Canvas canvas;
    public GameObject exitItember;
    SampleSoundManager sampleSoundManager;
    public RectTransform[] spawnPositions; // スポーン位置の配列
    ObjectManager objectManager;
    private GameObject newpageSpawn;
    public int spawnedItemCount = 0;

    void Start()
    {
        exitItember.SetActive(false);
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
        objectManager = FindObjectOfType<ObjectManager>();

        // 初期化: pageList を設定
        foreach (GameObject pageObj in pageList)
        {
            pageObj.SetActive(false); // 初めはすべて非表示
        }
    }

    void Update()
    {
        button.transform.position = handle.transform.position;
        SetItemsActive();
        UpdatePageVisibility();
   
    }

    public int GetItemCount()
    {
        return getItemList.Count; // list内のアイテムの個数を数える
    }

    public void OnItemBer()
    {
        GetItemCount();

        if (getItemList.Count > 0 && !OnBar)
        {
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE2);
            }
            image.sprite = OnBerImage;
            exitItember.SetActive(true);
            slider.value = getItemList.Count * 0.078f; // アイテムの個数に応じてアイテムバーを伸ばす
            OnBar = true;
        }
        else if (OnBar)
        {
            OffBer();
        }
        if (getItemList.Count == 0)
        {
            Debug.Log("アイテムが何もありません"); // アイテムがない場合は何もしない
        }
    }

    public void OffBer()
    {
        image.sprite = OffBerImage;
        slider.value = 0;
        exitItember.SetActive(false);
        OnBar = false;
    }

    public void AddItem(GameObject item)
    {
        getItemList.Add(item);
        itemGetPanel.SetActive(true);

        if (sampleSoundManager != null)
        {
            sampleSoundManager.PlaySe(SeType.SE1);
        }
        if (spawnedItemCount < getItemList.Count && spawnedItemCount < spawnPositions.Length)
        {
            // スポーン位置の配列から対応する位置を取得
            RectTransform targetPosition = spawnPositions[spawnedItemCount];

            // アイテムを対応する位置にスポーン
            GameObject spawnedObject = Instantiate(getItemList[spawnedItemCount], targetPosition.position, targetPosition.rotation);


            getItemList.Remove(item);
            getItemList.Add(spawnedObject);

            spawnedObject.transform.SetParent(spawnPositions[spawnedItemCount], false);

            // スポーン済みアイテムの数をインクリメント
            spawnedItemCount++;

            if (OnBar == true)
            {
                slider.value = getItemList.Count * 0.075f; // 一個目のオブジェクトは必ず非表示
            }
        }

    }

    void SetItemsActive()
    {
        foreach (GameObject item in getItemList)
        {
            item.SetActive(OnBar);
        }
    }

    void UpdatePageVisibility()
    {
        // spawnedItemCount に基づいてページオブジェクトの表示を更新
        for (int i = 0; i < pageList.Count; i++)
        {
            if (i < spawnedItemCount && i > 0)
            {
                pageList[i-1].SetActive(OnBar); // OnBar が true のときにのみ表示
                if (OnBar)
                {
                    // ページオブジェクトを最背面に移動させる
                    pageList[i].transform.SetAsLastSibling();
                }
            }
            else if((i > spawnedItemCount && i > 0))
            {
                pageList[i-1].SetActive(false); // spawnedItemCount より多いページは非表示
            }
        }
    }

    public void RemoveItem(GameObject itemToRemove)
    {
        if (getItemList.Contains(itemToRemove))
        {
            int indexToRemove = getItemList.IndexOf(itemToRemove);

            getItemList.Remove(itemToRemove); // アイテムリストから削除
            Destroy(itemToRemove); // アイテムのGameObjectを破棄


            // アイテムリストを再配置
            UpdateItemBar();

            // スポーン済みアイテムの数をデクリメント
            spawnedItemCount = Mathf.Max(0, getItemList.Count);
        }
        else
        {
            Debug.Log("アイテムリストからアイテムを消せません");
        }
    }

    private void UpdateItemBar()
    {
        // アイテムの再配置
        for (int i = 0; i < getItemList.Count; i++)
        {
            RectTransform targetPosition = spawnPositions[i];
            getItemList[i].transform.position = targetPosition.position;
            getItemList[i].transform.rotation = targetPosition.rotation;
        }

        // 背景の再配置
        for (int i = 0; i < pageList.Count; i++)
        {
            // 背景は2つ目のアイテムから表示するため、iは1から始める
            if (i < getItemList.Count - 1)
            {
                RectTransform pageTargetPosition = spawnPositions[i ];
                pageList[i].transform.position = pageTargetPosition.position;
                pageList[i].transform.rotation = pageTargetPosition.rotation;
                pageList[i].SetActive(true);
            }
            else
            {
                // 余分な背景は非表示にする
                pageList[i].SetActive(false);
            }
        }

        // アイテムバーの長さを更新
        slider.value = getItemList.Count * 0.055f;
    }
}