using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLight : MonoBehaviour
{
    Image spriteRenderer;

    public List<Sprite> rightSprite = new List<Sprite>();

    private Sprite nomalSprite;
  
    public enum ItemName
    {
        LeftMemo,
        RightMemo,
        MixMemo
    }
    [SerializeField]
    ItemName itemName;
    // Start is called before the first frame update
    void Start()
    {
       spriteRenderer = this.GetComponent<Image>();

        if (spriteRenderer != null)
        {
            nomalSprite = this.spriteRenderer.sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void ChangeLight()
    {
        if (itemName == ItemName.LeftMemo)
        {
            spriteRenderer.sprite = rightSprite[0];
        }
        if (itemName == ItemName.RightMemo)
        {
            spriteRenderer.sprite = rightSprite[1];
        }
        if (itemName == ItemName.MixMemo)
        {
            spriteRenderer.sprite = rightSprite[2];
        }
    }
    public void ChangeNomal()
    {
        if (itemName == ItemName.LeftMemo)
        {
            spriteRenderer.sprite = nomalSprite;
        }
        if (itemName == ItemName.RightMemo)
        {
            spriteRenderer.sprite = nomalSprite;
        }
        if (itemName == ItemName.MixMemo)
        {
            spriteRenderer.sprite = nomalSprite;
        }
    }
}
