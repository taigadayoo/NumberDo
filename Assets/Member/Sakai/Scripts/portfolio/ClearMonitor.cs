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
        SampleSoundManager.Instance.PlayBgm(BgmType.BGM3);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeCounter.isclier && Input.GetMouseButtonDown(0))
        {

            objectManager.addItemNum = 7;
            itemBer.AddItem(objectManager.items[objectManager.addItemNum]);
            objectManager.imageNum = 19;
            getSet.ImageChange(objectManager.imageNum);
           
            objectManager.miniGameClear.SetActive(false);
            objectManager.allColliderSwicth(true);
            this.gameObject.SetActive(false);
        }
    }

}
