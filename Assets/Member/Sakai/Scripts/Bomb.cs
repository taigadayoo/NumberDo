using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    SceneManagement sceneManagement;

    [SerializeField]
    ItemDragDrop dragDrop;

    private void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collion)
    {
        if (collion.gameObject.tag == "Key")
        {
            Debug.Log("aaa");
            sceneManagement.OnClear();
        }
    }
}
