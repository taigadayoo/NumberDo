using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOffCol : MonoBehaviour
{
    public GameObject book;
    public GameObject picture;
    public GameObject monitor;
    public GameObject medicine;
    public GameObject monitorPass;
    public GameObject miniGameZoom;
    public GameObject miniGameClear;
    public GameObject bombPass;
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
            medicine.SetActive(false);
            monitorPass.SetActive(false);
            this.gameObject.SetActive(false);
            objectManager.allColliderSwicth(true);
            miniGameClear.SetActive(false);
            bombPass.SetActive(false);
            objectManager.OnBox4 = false;
        }
    }
}
