using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    //�J�E���g�_�E��
    public float countdown = 5.0f;
    public bool isclier = false;
    public GameObject popup;
    public GameObject prefab;

    // Update is called once per frame
     void Update()
    {
        //���Ԃ��J�E���g�_�E������
        countdown -= Time.deltaTime;

        //countdown��0�ȉ��ɂȂ����Ƃ�
        if (countdown <= 0)
        {
            popup.SetActive(false);
            Debug.Log("�N���A");
        }

    }

 
}

