using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonCotroller : MonoBehaviour
{
    public GameObject popup;
    public GameObject prefab;

    void Start()
    {
        //非表示にしている
        popup.SetActive(false);
        prefab.SetActive(false);
    }

    public void OnButtonClick()
    {
        //ボタンを押したら表示する
        popup.SetActive(true);
        prefab.SetActive(true);
    }
}
