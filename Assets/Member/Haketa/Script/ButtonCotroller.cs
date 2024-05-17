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
    public GameObject buttons;
    ObjectManager objectManager;

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        //非表示にしている
        popup.SetActive(false);
        prefab.SetActive(false);
        buttons.SetActive(false);
    }

    public void OnButtonClick()
    {
        //ボタンを押したら表示する
        popup.SetActive(true);
        prefab.SetActive(true);
        buttons.SetActive(true);
        objectManager.targetObjectBox.SetActive(false);
        objectManager.targetObjectPass.SetActive(false);
    }
}
