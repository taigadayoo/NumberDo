using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaController : MonoBehaviour
{
    //Xの上限
    float xLimit = 5.5f;

    public void RbuttonClick()
    {
        transform.Translate(2, 0, 0);
    }

    public void LButtonClick()
    {
        transform.Translate(-2, 0, 0);
    }

    void Update()
    {
        //追加　現在のポジションを保持する
        Vector3 currentPos = transform.position;

        //追加　Mathf.ClampでX,Yの値それぞれが最小～最大の範囲内に収める。
        //範囲を超えていたら範囲内の値を代入する
        currentPos.x = Mathf.Clamp(currentPos.x, -xLimit, xLimit);

        //追加　positionをcurrentPosにする
        transform.position = currentPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ballprefab")) // 弾と衝突したらプレイヤーを消滅させる
        {
            gameObject.SetActive(false);

            //ゲーム内の時間を止める
            Time.timeScale = 0f;

            //GameOverSceneを呼び出す
            SceneManager.LoadScene("GameOverScene");

            Debug.Log("ゲームオーバー");
        }
    }
}
