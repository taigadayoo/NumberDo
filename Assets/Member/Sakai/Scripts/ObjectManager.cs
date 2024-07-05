using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // 特定のオブジェクトを参照するための変数
    public GameObject targetObjectBox;
    public GameObject targetObjectBox2;
    public GameObject targetObjectBox3;
    public GameObject targetObjectPass;
    public GameObject targetObjectBox4;
    public GameObject key;
    public List<GameObject> items = new List<GameObject>();
    public GameObject password;
   
    public GameObject gameCanvas;
    public GameObject itemGetPanel;
    public GameObject zoomShelf;

    public List<GameObject> touchObject = new List<GameObject>();
    private bool OnBox = false;
    private bool OnPass = false;
    private bool OnBox2 = false;
    private bool OnBox3 = false;
    private bool OnBox4 = false;
    public bool OnePassWord = false;
    public bool ItemGet = false;
    ItemBer itemBer;
    SampleSoundManager sampleSoundManager;
    ButtonCotroller buttonCotroller;
    Timer timer;
    Password passwordScripts;
    [SerializeField]
    ItemGetSet getSet;
   
    private void Start()
    {
        password.SetActive(false);
       
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
        itemBer = FindObjectOfType<ItemBer>();
        buttonCotroller = FindObjectOfType<ButtonCotroller>();
       timer =  FindFirstObjectByType<Timer>();
        zoomShelf.SetActive(false);
}
    void Update()
    {
        ObjectTouch();
     if(passwordScripts!= null)
        {
            passwordScripts = FindObjectOfType<Password>();
        }

    }
   public void DeactivateAllObjects()
    {
        foreach (GameObject obj in touchObject)
        {
            if (obj != null) // nullチェックを行う
            {
                obj.SetActive(false);
            }
        }
    }
   public void ActivateAllObjects()
    {
        foreach (GameObject obj in touchObject)
        {
            if (obj != null) // nullチェックを行う
            {
                obj.SetActive(true);
            }
        }
    }
    private void ObjectTouch()
    {
        
            if (Input.GetMouseButtonDown(0))
        {
            // マウスの位置からRayを飛ばす
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            // RayがCollider2Dに当たったかを検出する
            if (hit.collider != null && !ItemGet)
            {
                if (hit.collider.gameObject == targetObjectPass && !OnePassWord)
                {
                    if (OnPass == false)
                    {
                        if (sampleSoundManager != null)
                        {
                            SampleSoundManager.Instance.PlaySe(SeType.SE3);
                        }
                        password.SetActive(true);
                        zoomShelf.SetActive(false);
                        OnPass = true;
                    }
                    else
                    {
                        password.SetActive(false);
                        OnPass = false;
                    }
                }
                if (hit.collider.gameObject == targetObjectPass && OnePassWord && !ItemGet)
                {
                    itemBer.AddItem(key);
                    if (getSet != null)
                    {
                        getSet.ImageChange(0);
                        ItemGet = true;
                    }
                }
                if (hit.collider.gameObject == targetObjectBox4 && !OnBox4)
                {
                    zoomShelf.SetActive(true);
                    OnBox4 = true;
                }
                else if (hit.collider.gameObject == targetObjectBox4 && OnBox4)
                {
                    zoomShelf.SetActive(false);
                    OnBox4 = false;
                }
            }
            if (hit.collider != null && !OnBox4)
            {
                // 当たったCollider2DのGameObjectが特定のオブジェクトであるかを確認する
                if (hit.collider.gameObject == targetObjectBox)
                {
                    if (OnBox == false)
                    {
                        buttonCotroller.OnButtonClick();
                        OnBox = true;
                        timer.Stop();
                        if (sampleSoundManager != null)
                        {
                            SampleSoundManager.Instance.PlaySe(SeType.SE2);
                        }
                    }
                   
                }
                
                    if (hit.collider.gameObject == targetObjectBox2 && !OnBox2)
                {
                    itemBer.AddItem(items[0]);
                    if (getSet != null)
                    {
                        getSet.ImageChange(0);
                        ItemGet = true;
                    }

                    OnBox2 = true;
                }
                if (hit.collider.gameObject == targetObjectBox3 && !OnBox3)
                {
                    itemBer.AddItem(items[1]);
                    if (getSet != null)
                    {
                        getSet.ImageChange(1);
                        ItemGet = true;
                    }
                    OnBox3 = true;
                }
              
               
            }
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == itemGetPanel)
                {
                    itemGetPanel.SetActive(false);
                    ItemGet = false;
                }
            }
        }
        
    }
}
