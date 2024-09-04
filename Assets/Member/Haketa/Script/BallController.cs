using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    TimeCounter timeCounter;
    RectTransform rectTransform;
    
    public bool isTouch;
    void Start()
    {
        // RectTransform�R���|�[�l���g���擾
        rectTransform = GetComponent<RectTransform>();

        // TimeCounter��T���Ď擾
        timeCounter = FindObjectOfType<TimeCounter>();
      
    }

    void Update()
    {
        // UI�I�u�W�F�N�g�̈ʒu���ړ�
        rectTransform.anchoredPosition += new Vector2(0, -1.5f);
        transform.SetAsLastSibling();
        // y��-187.0f�ɂȂ����Ƃ��폜����
        if (rectTransform.anchoredPosition.y < -200f)
        {
            Destroy(gameObject);
        }

        // �N���A�������������ꂽ��폜����
        if (timeCounter.isclier == true)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
�@�@{
        //ball��player�ɓ���������Q�[���I�[�o�[�ɂ���
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("�Q�[���I�[�o�[");
        }
        else
        {
            this.isTouch = true;
        }
    }
}

