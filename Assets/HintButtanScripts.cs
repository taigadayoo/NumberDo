using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintButtanScripts : MonoBehaviour
{
    public GameObject hintPanel;
    ObjectManager objectManager;
    public bool onHint = false;
    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();   
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnHint()
    {
        if (!onHint)
        {
            hintPanel.SetActive(true);
            objectManager.allColliderSwicth(false);
            objectManager.allColliderZoomSwicth(false);
            objectManager.zoomOffColText.SetActive(true);
            objectManager.OnBox4 = true;
            onHint = true;
        }
        else
        {
            hintPanel.SetActive(false);
            objectManager.allColliderSwicth(true);
            objectManager.allColliderZoomSwicth(true);
            objectManager.zoomOffColText.SetActive(false);
            objectManager.OnBox4 = false;
            onHint = false;
        }
    }
}
