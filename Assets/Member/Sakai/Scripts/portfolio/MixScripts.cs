using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixButton : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    CanvasTouchMouse canvasTouchMouse;
    [SerializeField]
    ObjectManager objectManager;
    [SerializeField]
    ItemBer itemBer;
    // Update is called once per frame
    SampleSoundManager sampleSoundManager;
    [SerializeField]
    MixImageScripts mixImageScripts;
    [SerializeField]
    HintTextChange hintTextChange;
    ItemGetSet getSet;
    private void Start()
    {

        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
      
    }
    void Update()
    {
        if (getSet == null)
        {
            getSet = FindObjectOfType<ItemGetSet>();
        }
    }
    public void OnMix()
    {
        // 「LeftMemo」と「RightMemo」を組み合わせた場合の処理
        if (canvasTouchMouse.lastClickedObject.gameObject.tag == "LeftMemo" && canvasTouchMouse.previousClickedObject.gameObject.tag == "RightMemo" ||
            canvasTouchMouse.previousClickedObject.gameObject.tag == "LeftMemo" && canvasTouchMouse.lastClickedObject.gameObject.tag == "RightMemo")
        {
            // 組み合わせたアイテムを削除
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            // サウンド再生
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE1);
            }
            // 画像番号と新しいアイテムを設定
            objectManager.imageNum = 2;
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[2]);
            // ミックス画像を非表示に
            mixImageScripts.mixImage.enabled = false;
            // 選択されたアイテムをクリア
            canvasTouchMouse.lastClickedObject = null;
            canvasTouchMouse.previousClickedObject = null;
            // アイテムバーを更新
            itemBer.OnItemBer();
        }// 以下、同様のパターンが繰り返される
        else if (canvasTouchMouse.lastClickedObject.gameObject.tag == "Clock" && canvasTouchMouse.previousClickedObject.gameObject.tag == "Nabe" || canvasTouchMouse.previousClickedObject.gameObject.tag == "Clock" && canvasTouchMouse.lastClickedObject.gameObject.tag == "Nabe")
        {
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE1);
            }
            objectManager.imageNum = 16;
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[4]);
            mixImageScripts.mixImage.enabled = false;
            canvasTouchMouse.lastClickedObject = null;
            canvasTouchMouse.previousClickedObject = null;
            itemBer.OnItemBer();
        }
        else if (canvasTouchMouse.lastClickedObject.gameObject.tag == "Fruit" && canvasTouchMouse.previousClickedObject.gameObject.tag == "Conpas" || canvasTouchMouse.previousClickedObject.gameObject.tag == "Fruit" && canvasTouchMouse.lastClickedObject.gameObject.tag == "Conpas")
        {
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE1);
            }
           
            objectManager.imageNum = 18;
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[5]);
            mixImageScripts.mixImage.enabled = false;
            canvasTouchMouse.lastClickedObject = null;
            canvasTouchMouse.previousClickedObject = null;
            itemBer.OnItemBer();
        }
        else if (canvasTouchMouse.lastClickedObject.gameObject.tag == "Fruited" && canvasTouchMouse.previousClickedObject.gameObject.tag == "Light" || canvasTouchMouse.previousClickedObject.gameObject.tag == "Fruited" && canvasTouchMouse.lastClickedObject.gameObject.tag == "Light")
        {
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE1);
            }
            objectManager.imageNum = 21;
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[6]);
            mixImageScripts.mixImage.enabled = false;
            canvasTouchMouse.lastClickedObject = null;
            canvasTouchMouse.previousClickedObject = null;
            hintTextChange.ImageChange(2);
            itemBer.OnItemBer();
        }
        else if (canvasTouchMouse.lastClickedObject.gameObject.tag == "candle" && canvasTouchMouse.previousClickedObject.gameObject.tag == "Matti" || canvasTouchMouse.previousClickedObject.gameObject.tag == "candle" && canvasTouchMouse.lastClickedObject.gameObject.tag == "Matti")
        {
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE1);
            }
            objectManager.imageNum = 9;
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[9]);
            mixImageScripts.mixImage.enabled = false;
            canvasTouchMouse.lastClickedObject = null;
            canvasTouchMouse.previousClickedObject = null;
            hintTextChange.ImageChange(3);
            itemBer.OnItemBer();
        }
        else if (canvasTouchMouse.lastClickedObject.gameObject.tag == "candleFire" && canvasTouchMouse.previousClickedObject.gameObject.tag == "Koge" || canvasTouchMouse.previousClickedObject.gameObject.tag == "candleFire" && canvasTouchMouse.lastClickedObject.gameObject.tag == "Koge")
        {
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE1);
            }
            objectManager.imageNum = 11;
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[11]);
            itemBer.AddItem(objectManager.items[18]);
            mixImageScripts.mixImage.enabled = false;
            canvasTouchMouse.lastClickedObject = null;
            canvasTouchMouse.previousClickedObject = null;
            objectManager.recipeGet = true;
            hintTextChange.ImageChange(4);
            itemBer.OnItemBer();
        }
        else if (canvasTouchMouse.lastClickedObject.gameObject.tag == "candlestick" && canvasTouchMouse.previousClickedObject.gameObject.tag == "knife" || canvasTouchMouse.previousClickedObject.gameObject.tag == "candlestick" && canvasTouchMouse.lastClickedObject.gameObject.tag == "knife")
        {
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE1);
            }
            objectManager.imageNum = 14;
            getSet.ImageChange(objectManager.imageNum);

            itemBer.AddItem(objectManager.items[14]);
            mixImageScripts.mixImage.enabled = false;
            canvasTouchMouse.lastClickedObject = null;
            canvasTouchMouse.previousClickedObject = null;
            itemBer.OnItemBer();
        }
        else
        {
            Debug.Log("何も起きないようだ。");
        }
    }
}
