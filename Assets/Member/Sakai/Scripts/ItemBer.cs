using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBer : MonoBehaviour
{
    public Slider slider;
    public List<GameObject> getItemList = new List<GameObject>();
    public GameObject handle;
    public GameObject button;
    private int itemCount = 0;
    public bool OnBar = false;
    public bool ischak = false;
    public Canvas canvas;


    public Transform[] spawnPositions; // スポーン位置の配列

    private int spawnedItemCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        button.transform.position = handle.transform.position;
       
        SetItemsActive();
    }
    public int GetItemCount()
    {
        return getItemList.Count;//list内のアイテムの個数を数える
    }

    public void OnItemBer()
    {
       
        GetItemCount();

        if(getItemList.Count> 0 && !OnBar )
        {
           
            slider.value = getItemList.Count * 0.1f;　//アイテムの個数に応じてアイテムバーを伸ばす
            OnBar = true;
        }
       else if(OnBar)
        {
            slider.value = 0;
           
            OnBar = false;

        }
        if (getItemList.Count == 0)
        {
            Debug.Log("アイテムが何もありません");//アイテムがない場合は何もしない
        }
    }
    public void AddItem(GameObject item)
    {
        getItemList.Add(item);
        if(spawnedItemCount == 0)
        {
            item.SetActive(false);
        }
       
        if (spawnedItemCount < getItemList.Count && spawnedItemCount < spawnPositions.Length)
        {

            // スポーン位置の配列から対応する位置を取得
            Transform targetPosition = spawnPositions[spawnedItemCount];

            // アイテムを対応する位置にスポーン
            GameObject spawnedObject =  Instantiate(getItemList[spawnedItemCount], targetPosition.position, targetPosition.rotation);

            spawnedObject.transform.SetParent(canvas.transform, false);
        
        // スポーン済みアイテムの数をインクリメント
        spawnedItemCount++;

            if(OnBar == true)
            {
               slider.value = getItemList.Count * 0.1f;//一個目のオブジェクトは必ず非表示
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

   
}
