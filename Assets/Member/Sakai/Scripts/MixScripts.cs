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
    void Update()
    {

    }
    public void OnMix()
    {
        if (canvasTouchMouse.lastClickedObject.gameObject.tag == "LeftMemo" && canvasTouchMouse.previousClickedObject.gameObject.tag == "RightMemo" || canvasTouchMouse.previousClickedObject.gameObject.tag == "LeftMemo" && canvasTouchMouse.lastClickedObject.gameObject.tag == "RightMemo")
        {

          
            itemBer.spawnedItemCount--;
            itemBer.spawnedItemCount--;
            Destroy(canvasTouchMouse.lastClickedObject.gameObject);
            Destroy(canvasTouchMouse.previousClickedObject.gameObject);
            itemBer.getItemList.Remove(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.getItemList.Remove(canvasTouchMouse.previousClickedObject.gameObject);

            itemBer.AddItem(objectManager.items[2]);
        }
        else
        {
            Debug.Log("âΩÇ‡ãNÇ´Ç»Ç¢ÇÊÇ§ÇæÅB");
        }
    }
}
