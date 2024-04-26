using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private string sceneNameGame;
    [SerializeField] private string sceneNameClear;
    [SerializeField] private string sceneNameGameOver;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;
    // Start is called before the first frame update
 

    public void OnClear()
    {
        Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed);
    }
    public void OnGameOver()
    {
        Initiate.Fade(sceneNameGameOver, fadeColor, fadeSpeed);
    }
    public void OnStart()
    {
        Initiate.Fade(sceneNameGame, fadeColor, fadeSpeed);
    }
}
