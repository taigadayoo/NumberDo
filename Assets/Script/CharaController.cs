using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaController : MonoBehaviour
{
    public void RbuttonClick()
    {
        transform.Translate(2, 0, 0);
    }

    public void LButtonClick()
    {
        transform.Translate(-2, 0, 0);
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
