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
    private void Start()
    {
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
      
    }
    void Update()
    {

    }
    public void OnMix()
    {
        if (canvasTouchMouse.lastClickedObject.gameObject.tag == "LeftMemo" && canvasTouchMouse.previousClickedObject.gameObject.tag == "RightMemo" || canvasTouchMouse.previousClickedObject.gameObject.tag == "LeftMemo" && canvasTouchMouse.lastClickedObject.gameObject.tag == "RightMemo")
        {
            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
            itemBer.RemoveItem(canvasTouchMouse.previousClickedObject.gameObject);
            sampleSoundManager.PlaySe(SeType.SE1);
            itemBer.AddItem(objectManager.items[2]);
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
