using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixImageScripts : MonoBehaviour
{
    public Image mixImage;
    // Start is called before the first frame update

    [SerializeField]
    List<Sprite> mixItemImage = new List<Sprite>();
    [SerializeField]
    CanvasTouchMouse touchMouse;
    void Start()
    {
        mixImage = GetComponent<Image>();
        mixImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (touchMouse.lastClickedObject != null && touchMouse.previousClickedObject != null)
        {
            if (touchMouse.lastClickedObject.gameObject.tag == "LeftMemo" && touchMouse.previousClickedObject.gameObject.tag == "RightMemo" || touchMouse.previousClickedObject.gameObject.tag == "LeftMemo" && touchMouse.lastClickedObject.gameObject.tag == "RightMemo")
            {
                mixImage.enabled = true;
                mixImage.sprite = mixItemImage[0];
            }
            if (touchMouse.lastClickedObject.gameObject.tag == "Clock" && touchMouse.previousClickedObject.gameObject.tag == "Nabe" || touchMouse.previousClickedObject.gameObject.tag == "Clock" && touchMouse.lastClickedObject.gameObject.tag == "Nabe")
            {
                mixImage.enabled = true;
                mixImage.sprite = mixItemImage[1];
            }
            if (touchMouse.lastClickedObject.gameObject.tag == "Fruit" && touchMouse.previousClickedObject.gameObject.tag == "Conpas" || touchMouse.previousClickedObject.gameObject.tag == "Fruit" && touchMouse.lastClickedObject.gameObject.tag == "Conpas")
            {
                mixImage.enabled = true;
                mixImage.sprite = mixItemImage[2];
            }
            if (touchMouse.lastClickedObject.gameObject.tag == "Fruited" && touchMouse.previousClickedObject.gameObject.tag == "Light" || touchMouse.previousClickedObject.gameObject.tag == "Fruited" && touchMouse.lastClickedObject.gameObject.tag == "Light")
            {
                mixImage.enabled = true;
                mixImage.sprite = mixItemImage[3];
            }
            if (touchMouse.lastClickedObject.gameObject.tag == "candle" && touchMouse.previousClickedObject.gameObject.tag == "Matti" || touchMouse.previousClickedObject.gameObject.tag == "candle" && touchMouse.lastClickedObject.gameObject.tag == "Matti")
            {
                mixImage.enabled = true;
                mixImage.sprite = mixItemImage[4];
            }
            if (touchMouse.lastClickedObject.gameObject.tag == "candleFire" && touchMouse.previousClickedObject.gameObject.tag == "Koge" || touchMouse.previousClickedObject.gameObject.tag == "candleFire" && touchMouse.lastClickedObject.gameObject.tag == "Koge")
            {
                mixImage.enabled = true;
                mixImage.sprite = mixItemImage[5];
            }
            //else
            //{
            //    mixImage.enabled = false;
            //}
        }
    }
}
