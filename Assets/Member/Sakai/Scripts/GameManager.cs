using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
  
    SceneManagement sceneManagement;
  
    // Start is called before the first frame update
    void Start()
    {
        SampleSoundManager.Instance.PlayBgm(BgmType.BGM2);
    }

    // Update is called once per frame
    void Update()
    {
        //if(isGameOver)
        //{
        //    sceneManagement.OnGameOver();
        //}
    }
    
}
