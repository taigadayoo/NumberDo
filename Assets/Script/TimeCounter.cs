using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    //カウントダウン
    public float countdown = 5.0f;


    // Update is called once per frame
    void Update()
    {
        //時間をカウントダウンする
        countdown -= Time.deltaTime;

        //countdownが0以下になったとき
        if (countdown <= 0)
        {
            gameObject.SetActive(false);
            Debug.Log("クリア");
        }
    }
}

