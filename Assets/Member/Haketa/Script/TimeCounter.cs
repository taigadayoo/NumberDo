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

   public Sprite clierImage;
    [SerializeField]
    Image spriteRenderer;
    private void Start()
    {
        timer = FindFirstObjectByType<Timer>();
        objectManager = FindObjectOfType<ObjectManager>();
        soundManager = FindObjectOfType<SampleSoundManager>();
    }
    // Update is called once per frame
    void Update()
    {
        //時間をカウントダウンする
        countdown -= Time.deltaTime;

        //countdownが0以下になったとき
        if (countdown <= 0)
        {
            timer.Restart();
            //popup.SetActive(false);
            buttons.SetActive(false) ;
            isclier = true;
            player.SetActive(false);
            spriteRenderer.sprite = clierImage;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE5);
            }
        }

    }

 
}

