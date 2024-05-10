using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    bool isTouch = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.005f, 0);

        //yが-3.0fになったとき削除する
        if(transform.position.y < -3.0f)
        {
            Destroy(gameObject);
        }


    }


    void OnCollisionEnter2D(Collision2D collision)
　　{
        //ballがplayerに当たったらゲームオーバーにする
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("ゲームオーバー");
        }
        else
        {
            this.isTouch = true;
        }
    }
}

