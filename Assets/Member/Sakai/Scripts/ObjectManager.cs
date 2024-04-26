using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // 特定のオブジェクトを参照するための変数
    public GameObject targetObjectBox;

    public GameObject targetObjectPass;
    public GameObject key;
    public GameObject password;
    public GameObject judgeButton;
    public GameObject inputField;

    private bool OnBox = false;
    private bool OnPass = false;
    private void Start()
    {
        password.SetActive(false);
        judgeButton.SetActive(false);
        inputField.SetActive(false);
        key.SetActive(false);
}
    void Update()
    {
        ObjectTouch();
        

    }
    private void ObjectTouch()
    {
        
            if (Input.GetMouseButtonDown(0))
        {
            // マウスの位置からRayを飛ばす
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            // RayがCollider2Dに当たったかを検出する
            if (hit.collider != null)
            {
                // 当たったCollider2DのGameObjectが特定のオブジェクトであるかを確認する
                if (hit.collider.gameObject == targetObjectBox)
                {
                    if (OnBox == false)
                    {
                        password.SetActive(true);
                        OnBox = true;
                    }
                    else
                    {
                        password.SetActive(false);
                        OnBox = false;
                    }
                }
                if(hit.collider.gameObject == targetObjectPass)
                {
                    if (OnPass == false)
                    {
                        judgeButton.SetActive(true);
                        inputField.SetActive(true);
                        OnPass = true;
                    }
                    else
                    {
                        judgeButton.SetActive(false);
                        inputField.SetActive(false);
                        OnPass = false;
                    }
                }
            }
        }
        
    }
}
