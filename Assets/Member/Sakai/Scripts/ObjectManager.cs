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
    public GameObject targetObjectKey;
    public GameObject key;
    public List<GameObject> items = new List<GameObject>();
    public GameObject password;

    public GameObject gameCanvas;
    public GameObject itemGetPanel;
    public GameObject zoomShelf;
    public GameObject unrockingRock;

    public List<GameObject> touchObject = new List<GameObject>();
    private bool OnBox = false;
    public bool OnPass = false;
    private bool OnBox2 = false;
    private bool OnBox3 = false;
    public bool OnBox4 = false;
    private bool OneKey = false;
    public bool OnePassWord = false;
    public bool ItemGet = false;
    public bool Ontext = false;
    public bool unrocking = false;
    public int imageNum = 0;
    public int addItemNum = 0;
    ItemBer itemBer;
    SampleSoundManager sampleSoundManager;
    ButtonCotroller buttonCotroller;
    Timer timer;
    Password passwordScripts;
    [SerializeField]
    ItemGetSet getSet;
    [SerializeField]
    SimpleDialogueManager dialogueManager;
    private void Start()
    {
        password.SetActive(false);

        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
        itemBer = FindObjectOfType<ItemBer>();
        buttonCotroller = FindObjectOfType<ButtonCotroller>();
        timer = FindFirstObjectByType<Timer>();
        zoomShelf.SetActive(false);
    }
    void Update()
    {
        ObjectTouch();
        if (passwordScripts != null)
        {
            passwordScripts = FindObjectOfType<Password>();
        }
        if(zoomShelf.activeSelf)
        {
            Ontext = true;
        }
        else
        {
            Ontext = false;
        }
        //if(dialogueManager.chatEnd)
        //{
        //    getSet.ImageChange(imageNum);
        //    itemBer.AddItem(items[addItemNum]);
        //}

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
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == itemGetPanel)
                {
                    itemGetPanel.SetActive(false);
                    ItemGet = false;
                }
            }
            // RayがCollider2Dに当たったかを検出する
            if (hit.collider != null && !ItemGet )
            {
                if (hit.collider.gameObject == targetObjectPass && !OnePassWord )
                {
                    if (OnPass == false)
                    {
                        if (sampleSoundManager != null)
                        {
                            SampleSoundManager.Instance.PlaySe(SeType.SE3);
                        }
                        password.SetActive(true);
                        zoomShelf.SetActive(false);
                        Ontext = false;
                        OnPass = true;

                    }
                   
                }
               
                //if (hit.collider.gameObject == targetObjectPass && OnePassWord && !ItemGet)
                //{
                //    itemBer.AddItem(key);
                //    if (getSet != null)
                //    {
                //        imageNum = 3;
                //        ItemGet = true;
                //    }
                //}
                if (hit.collider.gameObject == targetObjectBox4 && !OnBox4)
                {
                    zoomShelf.SetActive(true);
                    OnBox4 = true;
                    Ontext = true;
                }
                else if (hit.collider.gameObject == targetObjectBox4 && OnBox4)
                {
                    zoomShelf.SetActive(false);
                    password.SetActive(false);
                    Ontext = false;
                    OnBox4 = false;
                }
                 if (hit.collider.gameObject == targetObjectKey && !OneKey)
                {
                    addItemNum = 3;
                    itemBer.AddItem(items[addItemNum]);
                    imageNum = 3;
                    getSet.ImageChange(imageNum);
                    OnPass = false;
                    OnePassWord = true;
                    OnBox4 = false;
                    if (sampleSoundManager != null)
                    {
                        SampleSoundManager.Instance.PlaySe(SeType.SE4);
                    }
                    unrockingRock.SetActive(false);
                    OneKey = true;
                }
            }
            if (hit.collider != null && !OnBox4 && !OnPass)
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
                    addItemNum = 0;
                    if (getSet != null)
                    {
                        imageNum = 0;
                        ItemGet = true;
                    }

                    OnBox2 = true;
                }
                if (hit.collider.gameObject == targetObjectBox3 && !OnBox3)
                {
                    addItemNum = 1;
                    if (getSet != null)
                    {
                        imageNum = 1;
                        ItemGet = true;
                    }
                    OnBox3 = true;
                }
               

            }
           
        }

    }
}