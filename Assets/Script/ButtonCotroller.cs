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
        //��\���ɂ��Ă���
        popup.SetActive(false);
        prefab.SetActive(false);
    }

    public void OnButtonClick()
    {
        //�{�^������������\������
        popup.SetActive(true);
        prefab.SetActive(true);
    }
}
