using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
  
    SceneManagement sceneManagement;

    SampleSoundManager sampleSoundManager;
    // Start is called before the first frame update
    void Start()
    {
        sampleSoundManager = FindFirstObjectByType<SampleSoundManager>();

        if(sampleSoundManager != null)
        {
            sampleSoundManager.StopBgm();

            sampleSoundManager.PlayBgm(BgmType.BGM2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            sceneManagement.OnGameOver();
        }
    }
    
}
