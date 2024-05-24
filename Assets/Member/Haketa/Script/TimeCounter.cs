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
        //���Ԃ��J�E���g�_�E������
        countdown -= Time.deltaTime;

        //countdown��0�ȉ��ɂȂ����Ƃ�
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

