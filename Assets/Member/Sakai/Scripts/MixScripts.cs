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
        if (canvasTouchMouse.lastClickedObject.gameObject.tag == "LeftMemo" && canvasTouchMouse.previousClickedObject.gameObject.tag == "RightMemo" || canvasTouchMouse.previousClickedObject.gameObject.tag == "LeftMemo" && canvasTouchMouse.lastClickedObject.gameObject.tag == "RightMemo")
        {
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE1);
            }
            objectManager.imageNum = 2;
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[2]);
            mixImageScripts.mixImage.enabled = false;
            canvasTouchMouse.lastClickedObject = null;
            canvasTouchMouse.previousClickedObject = null;
        }
       else if (canvasTouchMouse.lastClickedObject.gameObject.tag == "Clock" && canvasTouchMouse.previousClickedObject.gameObject.tag == "Nabe" || canvasTouchMouse.previousClickedObject.gameObject.tag == "Clock" && canvasTouchMouse.lastClickedObject.gameObject.tag == "Nabe")
        {
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE1);
            }
            objectManager.imageNum = 2;
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[4]);
            mixImageScripts.mixImage.enabled = false;
            canvasTouchMouse.lastClickedObject = null;
            canvasTouchMouse.previousClickedObject = null;
        }
        else if (canvasTouchMouse.lastClickedObject.gameObject.tag == "Fruit" && canvasTouchMouse.previousClickedObject.gameObject.tag == "Conpas" || canvasTouchMouse.previousClickedObject.gameObject.tag == "Fruit" && canvasTouchMouse.lastClickedObject.gameObject.tag == "Conpas")
        {
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE1);
            }
            objectManager.imageNum = 2;
            getSet.ImageChange(objectManager.imageNum);
            itemBer.AddItem(objectManager.items[5]);
            mixImageScripts.mixImage.enabled = false;
            canvasTouchMouse.lastClickedObject = null;
            canvasTouchMouse.previousClickedObject = null;
        }
        else
        {
            Debug.Log("âΩÇ‡ãNÇ´Ç»Ç¢ÇÊÇ§ÇæÅB");
        }
    }
}
