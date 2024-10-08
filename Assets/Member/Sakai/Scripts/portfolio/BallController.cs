using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    TimeCounter timeCounter;
    RectTransform rectTransform;
    
    public bool isTouch;
    public float speed = 20f;
    void Start()
    {
        // RectTransformコンポーネントを取得
        rectTransform = GetComponent<RectTransform>();

        // TimeCounterを探して取得
        timeCounter = FindObjectOfType<TimeCounter>();
      
    }

    void Update()
    {

      
        rectTransform.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);
        transform.SetAsLastSibling();
        // yが-187.0fになったとき削除する
        if (rectTransform.anchoredPosition.y < -200f)
        {
            Destroy(gameObject);
        }

        // クリア条件が満たされたら削除する
        if (timeCounter.isclier == true)
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

