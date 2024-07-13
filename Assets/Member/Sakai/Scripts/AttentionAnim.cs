using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttentionAnim : MonoBehaviour
{
    SceneManagement sceneManagement;
    // Start is called before the first frame update
    void Start()
    {
        sceneManagement = FindFirstObjectByType<SceneManagement>();
        StartCoroutine(AttentionAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator AttentionAnimation()
    {

        yield return new WaitForSeconds(1f);

        sceneManagement.OnAttention();
    }
}
