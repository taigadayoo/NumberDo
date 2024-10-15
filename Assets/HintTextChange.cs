using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTextChange : MonoBehaviour
{

    public List<Sprite> itemImages = new List<Sprite>();

    [SerializeField]
    Image image;

    [SerializeField]
    SimpleDialogueManager dialogueManager;
    // Start is called before the first frame update


    public void ImageChange(int listNum)
    {
        if (image != null)
        {
            image.sprite = itemImages[listNum];
        }
    }
}
