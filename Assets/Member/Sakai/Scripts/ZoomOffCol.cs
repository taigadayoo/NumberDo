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
    Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        timer = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            objectManager.allColliderSwicth(false);
            objectManager.onZoom = true;
        }
    }
    private void OnMouseDown()
    {
        if (objectManager.textEnd)
        {
            objectManager.onZoom = false;
            objectManager.allColliderSwicth(true);
            book.SetActive(false);
            picture.SetActive(false);
            monitor.SetActive(false);
            medicine.SetActive(false);
            monitorPass.SetActive(false);
            this.gameObject.SetActive(false);
            miniGameClear.SetActive(false);
            bombPass.SetActive(false);
            objectManager.OnBox4 = false;
            objectManager.colDeley = true;
        }
    }
}
