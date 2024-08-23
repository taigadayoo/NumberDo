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
    void OnEnable()
    {
        countdown = 13f;
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԃ��J�E���g�_�E������
        countdown -= Time.deltaTime;

        //countdown��0�ȉ��ɂȂ����Ƃ�
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
            objectManager.miniGameClear.SetActive(true);
            objectManager.allColliderSwicth(true);
            objectManager.OnKeyCode = false;
            if (soundManager != null)
            {
                soundManager.PlaySe(SeType.SE5);
            }
        }

    }

 
}

