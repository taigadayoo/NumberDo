using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestList : MonoBehaviour
{
    [SerializeField]
   private ItemList itemList;
  
    [SerializeField]
    private Text nameText;

    private int itemNum = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nameText.text =itemList.ItemEnt[itemNum].itemName;

            itemNum++;
        }
    }
}
