using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private string sceneNameGame;
    [SerializeField] private string sceneNameClear;
    [SerializeField] private string sceneNameGameOver;
    [SerializeField] private string sceneNameTitle;
    [SerializeField] private string sceneNameAttention;
    [SerializeField] private string sceneNameScenario;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    private bool oneStart = false;
    private bool oneAttention = false;
    public static SceneManagement Instance { get; private set; }

    private void Awake()
    {
        // すでにインスタンスが存在する場合、破棄する
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // このインスタンスを静的インスタンスに設定する
        Instance = this;

        // シーンをまたいでもこのオブジェクトを破棄しない
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    private void Start()
    {
        oneStart = false;
        oneAttention = false; 
    }

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
        if (!oneStart)
        {
            Initiate.Fade(sceneNameAttention, fadeColor, fadeSpeed);
            oneStart = true;
        }
    }
    public void OnGame()
    {
            Initiate.Fade(sceneNameGame, fadeColor, fadeSpeed);

    }
    public void OnAttention()
    {

            Initiate.Fade(sceneNameScenario, fadeColor, fadeSpeed);
            oneAttention = true;

    }
    public void OnTitle()
    {
        Initiate.Fade(sceneNameTitle, fadeColor, fadeSpeed);
    }
  
}
