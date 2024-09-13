using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    //カウントダウン
    public float countdown = 13f;
    public bool isclier = false;
    public GameObject popup;
    public GameObject prefab;
    public GameObject buttons;
    public GameObject player;
    ObjectManager objectManager;
    Timer timer;
    SampleSoundManager soundManager;

    private bool OneClear = false;
   public Sprite clierImage;
    [SerializeField]
    Image spriteRenderer;
    private void Start()
    {
        timer = FindFirstObjectByType<Timer>();
        objectManager = FindObjectOfType<ObjectManager>();
        soundManager = FindObjectOfType<SampleSoundManager>();
      
    }
    void OnEnable()
    {
        countdown = 13f;
    }

    // Update is called once per frame
    void Update()
    {
        //時間をカウントダウンする
        countdown -= Time.deltaTime;

        //countdownが0以下になったとき
        if (countdown <= 0)
        {
            //timer.Restart();
            //popup.SetActive(false);
            buttons.SetActive(false) ;
            isclier = true;
            player.SetActive(false);
            spriteRenderer.sprite = clierImage;
            objectManager.OnMiniGame = false;
            transform.parent.gameObject.SetActive(false);
            if (!OneClear)
            {
                objectManager.miniGameClear.SetActive(true);
                SampleSoundManager.Instance.StopBgm();
                SampleSoundManager.Instance.PlaySe(SeType.SE13);
                OneClear = true;
            }
            objectManager.monitor.SetActive(false);
            objectManager.monitorGamed.SetActive(true);
            objectManager.allColliderSwicth(true);
            objectManager.OnKeyCode = false;
     
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE5);
            }
        }

    }

 
}

