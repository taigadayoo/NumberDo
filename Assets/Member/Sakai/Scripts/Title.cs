using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField]
    SceneManagement sceneManagement;
    [SerializeField]
    TitleAnim anim;
    // Start is called before the first frame update
    void Start()
    {
        SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
    }

    // Update is called once per frame
    async void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SampleSoundManager.Instance.StopBgm();
            await anim.TitleAnimation();
            sceneManagement.OnStart();
           
        }
    }
}
