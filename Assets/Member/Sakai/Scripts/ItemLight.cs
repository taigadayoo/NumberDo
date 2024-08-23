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
        Key,
        Nabe,
        Fruit,
        Clock,
        Light,
        Conpas,
        Fruited,
        BrabkLight,
        Matti,
        Candle,
        CandleFire,
        Koge,
        Recipe
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
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.RightMemo)
        {
            spriteRenderer.sprite = rightSprite[1];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.MixMemo)
        {
            spriteRenderer.sprite = rightSprite[2];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Key)
        {
            spriteRenderer.sprite = rightSprite[3];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Nabe)
        {
            spriteRenderer.sprite = rightSprite[4];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Fruit)
        {
            spriteRenderer.sprite = rightSprite[5];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Clock)
        {
            spriteRenderer.sprite = rightSprite[6];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Light)
        {
            spriteRenderer.sprite = rightSprite[7];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Conpas)
        {
            spriteRenderer.sprite = rightSprite[8];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Fruited)
        {
            spriteRenderer.sprite = rightSprite[9];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.BrabkLight)
        {
            spriteRenderer.sprite = rightSprite[10];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Matti)
        {
            spriteRenderer.sprite = rightSprite[10];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Candle)
        {
            spriteRenderer.sprite = rightSprite[11];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.CandleFire)
        {
            spriteRenderer.sprite = rightSprite[12];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Koge)
        {
            spriteRenderer.sprite = rightSprite[13];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Recipe)
        {
            spriteRenderer.sprite = rightSprite[14];
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
    }
    public void ChangeNomal()
    {
        if (itemName == ItemName.LeftMemo)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.RightMemo)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.MixMemo)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Key)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Nabe)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Fruit)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Clock)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Light)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Conpas)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Fruited)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.BrabkLight)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Matti)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Candle)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.CandleFire)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Koge)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
        if (itemName == ItemName.Recipe)
        {
            spriteRenderer.sprite = nomalSprite;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE3);
            }
        }
    }
}
