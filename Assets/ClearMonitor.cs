using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMonitor : MonoBehaviour
{
    [SerializeField]
    ObjectManager objectManager;
    [SerializeField]
    ItemBer itemBer;
    [SerializeField]
    ItemGetSet getSet;
    [SerializeField]
    TimeCounter timeCounter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timeCounter.isclier && Input.GetMouseButtonDown(0))
        {

            objectManager.addItemNum = 7;
            itemBer.AddItem(objectManager.items[objectManager.addItemNum]);
            objectManager.imageNum = 3;
            getSet.ImageChange(objectManager.imageNum);
          
            objectManager.miniGameClear.SetActive(false);
            objectManager.allColliderSwicth(true);
            this.gameObject.SetActive(false);
        }
    }
    //private void OnMouseDown()
    //{
    //   objectManager.addItemNum = 7;
    //    itemBer.AddItem(objectManager.items[objectManager.addItemNum]);
    //   objectManager.imageNum = 3;
    //    getSet.ImageChange(objectManager.imageNum);
    //    timeCounter.isclier = false;
    //   objectManager.miniGameClear.SetActive(false);
    //   objectManager.allColliderSwicth(false);
    //    this.gameObject.SetActive(false);
    //}

}
