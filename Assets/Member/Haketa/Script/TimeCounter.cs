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
    public float countdown = 5.0f;
    public bool isclier = false;
    public GameObject popup;
    public GameObject prefab;
    public GameObject passWord;
    public GameObject buttons;
    ObjectManager objectManager;
    Timer timer;
    private void Start()
    {
        timer = FindFirstObjectByType<Timer>();
        objectManager = FindObjectOfType<ObjectManager>();
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
            popup.SetActive(false);
            passWord.SetActive(true);
            buttons.SetActive(false) ;
            objectManager.targetObjectBox.SetActive(true);
            objectManager.targetObjectPass.SetActive(true);
            isclier = true;
            SampleSoundManager.Instance.PlaySe(SeType.SE5);
        }

    }

 
}

