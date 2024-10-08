using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneAuto());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            SampleSoundManager.Instance.StopBgm();
            SceneManagement.Instance.OnTitle();
        }
    }
    IEnumerator SceneAuto()
    {
        yield return new WaitForSeconds(3f);
        SampleSoundManager.Instance.StopBgm();
        SceneManagement.Instance.OnTitle();
    }
}
