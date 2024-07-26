using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public enum GameName
    {
        tutorial,
        mainGame
    }
    [SerializeField]
    GameName gameName;
    // 特定のオブジェクトを参照するための変数
    public GameObject targetObjectBox;
    public GameObject targetObjectBox2;
    public GameObject targetObjectBox3;
    public GameObject targetObjectPass;
    public GameObject targetObjectBox4;
    public GameObject targetObjectKey;
    public GameObject passKey;
    public GameObject key;
    public List<GameObject> items = new List<GameObject>();
    public GameObject password;
    public GameObject zoomoffCol;
    public GameObject gameCanvas;
    public GameObject itemGetPanel;
    public GameObject zoomShelf;
    public GameObject unrockingRock;

    public GameObject medicine;
    public GameObject clock;
    public GameObject fruit;

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
    public bool textEnd = false;

    public bool OnMedicine = false;
    public bool Onclock = false;
    public bool OnFruit = false;
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
   private List<Collider2D> allColliders = new List<Collider2D>();
    private void Start()
    {
        if (gameName == GameName.tutorial)
        {
            password.SetActive(false);
            zoomShelf.SetActive(false);
        }

        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
        itemBer = FindObjectOfType<ItemBer>();
        buttonCotroller = FindObjectOfType<ButtonCotroller>();
        timer = FindFirstObjectByType<Timer>();

        // 各オブジェクトからコライダーを取得してリストに追加
        foreach (GameObject obj in touchObject)
        {
            Collider2D[] colliders = obj.GetComponents<Collider2D>();
            allColliders.AddRange(colliders);
        }
     
    }
    void Update()
    {

        if (gameName == GameName.tutorial)
        {
            ObjectTouch();
            if (passwordScripts != null)
            {
                passwordScripts = FindObjectOfType<Password>();
            }
            if (zoomShelf.activeSelf)
            {
                Ontext = true;
            }
            else
            {
                Ontext = false;
            }

            if (unrockingRock.activeSelf)
            {
                Ontext = true;
            }
            else
            {
                Ontext = false;
            }
        }

        if (gameName == GameName.mainGame)
        {
            MainObjectTouch();
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
   public void allColliderSwicth(bool isEnabled)
    {
        foreach (Collider2D collider in allColliders)
        {
            collider.enabled = isEnabled;
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
                if (hit.collider.gameObject == targetObjectPass && !OnePassWord && textEnd)
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
                        allColliderSwicth(true);
                    }
                   
                }
               if(hit.collider.gameObject == zoomoffCol && textEnd)
                {
                    zoomShelf.SetActive(false);
                    Ontext = false;
                    OnBox4 = false;
                    OnPass = false;
                    allColliderSwicth(true);
                }
            
                if (hit.collider.gameObject == targetObjectBox4 && !OnBox4 && !Ontext)
                {
                    allColliderSwicth(false);
                    zoomShelf.SetActive(true);
                    OnBox4 = true;
                    Ontext = true;
                }
                else if (hit.collider.gameObject == targetObjectBox4 && OnBox4 && !Ontext)
                {
                    zoomShelf.SetActive(false);
                    password.SetActive(false);
                    Ontext = false;
                    OnBox4 = false;
                    OnPass = false;
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
            if (hit.collider != null && !OnBox4 && !OnPass && !ItemGet)
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
    private void MainObjectTouch()
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

            if (hit.collider != null && !OnBox4 && !OnPass && !ItemGet)
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

                if (hit.collider.gameObject == medicine && !OnMedicine)
                {

                    addItemNum = 0;
                    if (getSet != null)
                    {
                        imageNum = 0;
                        ItemGet = true;
                    }

                    OnMedicine = true;
                }
                if (hit.collider.gameObject == clock && !Onclock)
                {

                    addItemNum = 1;
                    if (getSet != null)
                    {
                        imageNum = 1;
                        ItemGet = true;
                    }
                    Onclock = true;
                }
                if (hit.collider.gameObject == fruit && !OnFruit)
                {

                    addItemNum = 3;
                    if (getSet != null)
                    {
                        imageNum = 1;
                        ItemGet = true;
                    }
                    OnFruit = true;
                }

            }

        }

    }
}