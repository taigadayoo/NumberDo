using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    bool isTouch = false;

    TimeCounter timeCounter;
    // Start is called before the first frame update
    void Start()
    {
       timeCounter =  FindObjectOfType<TimeCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.02f, 0);

        //y��-3.0f�ɂȂ����Ƃ��폜����
        if(transform.position.y < -3.0f)
        {
            Destroy(gameObject);
        }
        if(timeCounter.isclier == true)
        {
            Destroy(this.gameObject);
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

