using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonCotroller : MonoBehaviour
{
    public GameObject popup;

    void Start()
    {
        popup.SetActive(false);
    }

    public void OnButtonClick()
    {
        popup.SetActive(true);
    }
}
