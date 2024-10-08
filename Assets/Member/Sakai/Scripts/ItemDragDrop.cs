using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private bool test = false;

    enum Item
    {
        Key,
        Box
    }
    [SerializeField]
    Item item;
    [SerializeField]
    SceneManagement sceneManagement;

    void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        if(test == true)
        {
            sceneManagement.OnClear();
        }
    }

    void Update()
    {
      
        if (isDragging)
        {
            Vector3 mousePos = GetMouseWorldPosition();
            gameObject.transform.position = mousePos + offset;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb" && item == Item.Key )
        {
            test = true;
           
        }
        else
        {
            test = false;
        }
    }
}
