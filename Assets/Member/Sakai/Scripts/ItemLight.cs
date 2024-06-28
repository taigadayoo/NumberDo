using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLight : MonoBehaviour
{
    Image spriteRenderer;

    public List<Sprite> rightSprite = new List<Sprite>();

    private Sprite nomalSprite;
    SampleSoundManager soundManager;
    public enum ItemName
    {
        LeftMemo,
        RightMemo,
        MixMemo,
        Key
    }
    [SerializeField]
    ItemName itemName;
    // Start is called before the first frame update
    void Start()
    {
       spriteRenderer = this.GetComponent<Image>();
        soundManager = FindObjectOfType<SampleSoundManager>();
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
            soundManager.PlaySe(SeType.SE3);
        }
        if (itemName == ItemName.RightMemo)
        {
            spriteRenderer.sprite = rightSprite[1];
            soundManager.PlaySe(SeType.SE3);
        }
        if (itemName == ItemName.MixMemo)
        {
            spriteRenderer.sprite = rightSprite[2];
            soundManager.PlaySe(SeType.SE3);
        }
        if (itemName == ItemName.Key)
        {
            spriteRenderer.sprite = rightSprite[3];
            soundManager.PlaySe(SeType.SE3);
        }
    }
    public void ChangeNomal()
    {
        if (itemName == ItemName.LeftMemo)
        {
            spriteRenderer.sprite = nomalSprite;
            soundManager.PlaySe(SeType.SE3);
        }
        if (itemName == ItemName.RightMemo)
        {
            spriteRenderer.sprite = nomalSprite;
            soundManager.PlaySe(SeType.SE3);
        }
        if (itemName == ItemName.MixMemo)
        {
            spriteRenderer.sprite = nomalSprite;
            soundManager.PlaySe(SeType.SE3);
        }
        if (itemName == ItemName.Key)
        {
            spriteRenderer.sprite = nomalSprite;
            soundManager.PlaySe(SeType.SE3);
        }
    }
}
