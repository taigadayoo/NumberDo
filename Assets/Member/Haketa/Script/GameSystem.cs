using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{ 
   public bool isCalledOnce = false;
    [SerializeField]
    SceneManagement sceneManagement;

    public void GameOver()
    {
        if(!isCalledOnce) 
        {
            //isCalledOnce = true;
            //sceneManagement.OnBadEnd1();
        }
    }
}
