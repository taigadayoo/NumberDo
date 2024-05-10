using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{ 
    bool isCalledOnce = false;


    public void GameOver()
    {
        if(!isCalledOnce) 
        {
            isCalledOnce = true;
        }
    }
}
