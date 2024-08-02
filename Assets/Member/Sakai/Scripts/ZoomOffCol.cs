using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOffCol : MonoBehaviour
{
    public GameObject book;
    public GameObject picture;
    public GameObject monitor;
    ObjectManager objectManager;
    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (objectManager.textEnd)
        {
            book.SetActive(false);
            picture.SetActive(false);
            monitor.SetActive(false);
            this.gameObject.SetActive(false);
            objectManager.allColliderSwicth(true);
        }
    }
}
