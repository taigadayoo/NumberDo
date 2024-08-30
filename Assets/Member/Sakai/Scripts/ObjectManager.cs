using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("チュートリアルで触るもの")]
    public GameObject targetObjectBox;
    public GameObject targetObjectBox2;
    public GameObject targetObjectBox3;
    public GameObject targetObjectPass;
    public GameObject targetObjectBox4;
    public GameObject targetObjectKey;
    public GameObject passKey;
    public GameObject key;
    public List<GameObject> items = new List<GameObject>();
    [Header("ズームしたときのやつ")]
    public GameObject password;
    public GameObject zoomoffCol;
    public GameObject gameCanvas;
    public GameObject itemGetPanel;
    public GameObject zoomShelf;
    public GameObject unrockingRock;
    [Header("メインゲームで触るもの")]
    public GameObject bookZoom;
    public GameObject pictureZoom;
    public GameObject monitorZoom;
    public GameObject zoomOffColMain;
    public GameObject shelfZoom;
    public GameObject miniGameZoom;
    public GameObject monitorPass;
    public GameObject miniGame;


    public GameObject medicine;
    public GameObject clock;
    public GameObject fruit;
    public GameObject bookShelf;
    public GameObject picture;
    public GameObject monitor;
    public GameObject lightobj;
    public GameObject nabeobj;
    public GameObject touchPicture;
    public GameObject monitorRock;
    public GameObject miniGameClear;
    public GameObject miniGameDead;
    public GameObject candle;
    public GameObject candleNomal;
    public GameObject redBook;
    public GameObject poizon;
    public GameObject poizonCandle;
    public GameObject poizonKnife;

    public List<GameObject> touchObject = new List<GameObject>();

    public List<GameObject> zoomObject = new List<GameObject>();
    private bool OnBox = false;
    public bool OnPass = false;
    private bool OnBox2 = false;
    private bool OnBox3 = false;
    private Image pictureImage;
    public bool OnBox4 = false;
    private bool OneKey = false;
    public bool OnePassWord = false;
    public bool ItemGet = false;
    public bool Ontext = false;
    public bool unrocking = false;
    public bool textEnd = false;
    public bool oneLight = false;

    public bool OnMedicine = false;
    public bool Onclock = false;
    public bool OnFruit = false;
    public bool OnMiniGame = false;
    public bool OnKeyCode = false;
    public int imageNum = 0;
    public int addItemNum = 0;
    public bool OnClock = false;
    public Sprite lightImage;
    ItemBer itemBer;
    SampleSoundManager sampleSoundManager;
    ButtonCotroller buttonCotroller;
    Timer timer;
    Password passwordScripts;
    [SerializeField]
    ItemGetSet getSet;
    [SerializeField]
    SimpleDialogueManager dialogueManager;
    CanvasTouchMouse canvasTouchMouse;
    [SerializeField]
    TimeCounter timeCounter;




    private List<Collider2D> allColliders = new List<Collider2D>();
    private List<Collider2D> allZoomColliders = new List<Collider2D>();
    private void Start()
    {
        if (gameName == GameName.tutorial)
        {
            password.SetActive(false);
            zoomShelf.SetActive(false);
        }
        canvasTouchMouse = FindObjectOfType<CanvasTouchMouse>();
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
        itemBer = FindObjectOfType<ItemBer>();
        buttonCotroller = FindObjectOfType<ButtonCotroller>();
        timer = FindFirstObjectByType<Timer>();
        pictureImage = touchPicture.GetComponent<Image>();
        // 各オブジェクトからコライダーを取得してリストに追加
        foreach (GameObject obj in touchObject)
        {
            Collider2D[] colliders = obj.GetComponents<Collider2D>();
            allColliders.AddRange(colliders);
        }
        foreach (GameObject obj in zoomObject)
        {
            Collider2D[] colliders = obj.GetComponents<Collider2D>();
            allZoomColliders.AddRange(colliders);
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
            if (!canvasTouchMouse.isCandleSelected && !canvasTouchMouse.isKnifeSelected)
            {
                poizon.SetActive(true);
                poizonCandle.SetActive(false);
                poizonKnife.SetActive(false);
            }
            else if (canvasTouchMouse.isCandleSelected)
            {
                poizon.SetActive(false);
                poizonCandle.SetActive(true);
                poizonKnife.SetActive(false);
            }
            else if (canvasTouchMouse.isKnifeSelected)
            {
                poizon.SetActive(false);
                poizonCandle.SetActive(false);
                poizonKnife.SetActive(true);
            }
            if(textEnd)
            {
                allColliderZoomSwicth(true);
            }
            else
            {
                allColliderZoomSwicth(false);
            }

        }
        if(OnMiniGame && textEnd)
        {
            miniGame.SetActive(true);
            miniGameZoom.SetActive(false);
            allColliderSwicth(false);
           
           
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
    public void ActivateAllZoomObjects()
    {
        foreach (GameObject obj in zoomObject)
        {
            if (obj != null) // nullチェックを行う
            {
                obj.SetActive(true);
            }
        }
    }
    public void allColliderZoomSwicth(bool isEnabled)
    {
        foreach (Collider2D collider in allZoomColliders)
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
                        //timer.Stop();
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

                if (hit.collider.gameObject == nabeobj && !OnMedicine)
                {
                    getSet.ImageChange(5);
                    itemBer.AddItem(items[0]);
                    if (getSet != null)
                    {
                        imageNum = 5;
                        ItemGet = true;
                    }
                    nabeobj.SetActive(false);
                    OnMedicine = true;
                }
                if (hit.collider.gameObject == clock && !Onclock)
                {
                    OnClock = true;
                    addItemNum = 1;
                    if (getSet != null)
                    {
                        imageNum = 4;
                        ItemGet = true;
                    }
                   
                    Onclock = true;
                }
                if (hit.collider.gameObject == fruit && !OnFruit)
                {

                    StartCoroutine(FruitTouch());
                    if (getSet != null)
                    {
                        imageNum = 20;
                        ItemGet = true;
                    }
                    fruit.SetActive(false);
                    OnFruit = true;
                }
                if (hit.collider.gameObject == lightobj)
                {

                    getSet.ImageChange(17);
                    itemBer.AddItem(items[2]);
                    if (getSet != null)
                    {
                        imageNum = 17;
                        ItemGet = true;
                    }
                    oneLight = true;
                    lightobj.SetActive(false);
                }
                if (hit.collider.gameObject == candle)
                {
                    candleNomal.SetActive(false);
                    candle.SetActive(false);
                    getSet.ImageChange(7);
                    itemBer.AddItem(items[8]);
                    oneLight = true;

                }
                if (hit.collider.gameObject == redBook)
                {

                    addItemNum = 10;
                    if (getSet != null)
                    {
                        imageNum = 10;
                        ItemGet = true;
                    }
                    oneLight = true;

                }
                if (hit.collider.gameObject == touchPicture)
                {
                    if(canvasTouchMouse.isLightSelected)
                    {
                        itemBer.OffBer();
                        pictureImage.sprite = lightImage;
                        if (canvasTouchMouse.lastClickedObject != null)
                        {
                            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
                        }
                        canvasTouchMouse.lastClickedObject = null;
                    }
                   

                }
                if (hit.collider.gameObject == poizonKnife)
                {
                    if (canvasTouchMouse.isKnifeSelected)
                    {
                        getSet.ImageChange(15);
                        itemBer.AddItem(items[15]);
                        if (canvasTouchMouse.lastClickedObject != null)
                        {
                            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
                        }
                        canvasTouchMouse.lastClickedObject = null;
                    }
                }
                    if (hit.collider.gameObject == poizonCandle)
                {
                    if (canvasTouchMouse.isCandleSelected)
                    {
                        getSet.ImageChange(12);
                        itemBer.AddItem(items[12]);
                        if (canvasTouchMouse.lastClickedObject != null)
                        {
                            itemBer.RemoveItem(canvasTouchMouse.lastClickedObject.gameObject);
                        }
                        canvasTouchMouse.lastClickedObject = null;
                    }
                  

                }
                if (hit.collider.gameObject == bookShelf)
                {
                    bookZoom.SetActive(true);
                    zoomOffColMain.SetActive(true);
                    allColliderSwicth(false);
                    if(!oneLight && lightobj != null)
                    {
                        lightobj.SetActive(true);
                    }
                }
                if (hit.collider.gameObject == medicine)
                {
                    shelfZoom.SetActive(true);
                    zoomOffColMain.SetActive(true);
                    allColliderSwicth(false);
                    if (!OnMedicine && nabeobj != null)
                    {
                        nabeobj.SetActive(true);
                    }
                }
                if (hit.collider.gameObject == picture)
                {
                    pictureZoom.SetActive(true);
                    zoomOffColMain.SetActive(true);
                    allColliderSwicth(false);
                }
                if (hit.collider.gameObject == monitor)
                {
                    if (!OnKeyCode)
                    {
                        monitorZoom.SetActive(true);
                        zoomOffColMain.SetActive(true);
                        allColliderSwicth(false);
                    }
                    else
                    {
                        miniGame.SetActive(true);
                        allColliderSwicth(false);
                    }
                }
                if (hit.collider.gameObject == monitorRock && !timeCounter.isclier)
                {
                    monitorPass.SetActive(true);
                    monitorZoom.SetActive(false);
                    zoomOffColMain.SetActive(true);
                    allColliderSwicth(false);
                }
            
            }

        }

    }
    IEnumerator FruitTouch()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        getSet.ImageChange(20);
        itemBer.AddItem(items[3]);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        getSet.ImageChange(13);
        itemBer.AddItem(items[13]);

        textEnd = true;
        Ontext = false;
    }
}