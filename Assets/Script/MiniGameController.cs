using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public GameObject popup;

    public void OnMiniGameFinished()
    {
        popup.SetActive(false);
    }
}
