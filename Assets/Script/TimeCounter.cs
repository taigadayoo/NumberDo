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

    // Update is called once per frame
     void Update()
    {
        //時間をカウントダウンする
        countdown -= Time.deltaTime;

        //countdownが0以下になったとき
        if (countdown <= 0)
        {
            popup.SetActive(false);
            Debug.Log("クリア");
        }

    }

 
}

