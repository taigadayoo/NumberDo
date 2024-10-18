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
    public GameObject HintPanel;
    ObjectManager objectManager;
    Timer timer;
    HintButtanScripts hint;
    // Start is called before the first frame update
    void Start()
    {
        hint = FindObjectOfType<HintButtanScripts>();
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
          
  
            objectManager.allColliderZoomSwicth(true);
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
            HintPanel.SetActive(false);
            hint.onHint = false;
        }
    }
}
