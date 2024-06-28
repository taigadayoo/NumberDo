using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetSet : MonoBehaviour
{

  public  List<Sprite> itemImages = new List<Sprite>();

    [SerializeField]
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
  public void ImageChange(int listNum)
    {
        if (image != null)
        {
            image.sprite = itemImages[listNum];
        }
    }
}
