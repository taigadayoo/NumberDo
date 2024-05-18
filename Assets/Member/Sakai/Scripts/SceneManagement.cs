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
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;
    public static SceneManagement Instance { get; private set; }

    private void Awake()
    {
        // ���łɃC���X�^���X�����݂���ꍇ�A�j������
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ���̃C���X�^���X��ÓI�C���X�^���X�ɐݒ肷��
        Instance = this;

        // �V�[�����܂����ł����̃I�u�W�F�N�g��j�����Ȃ�
        DontDestroyOnLoad(gameObject);
    }
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
    public void OnTitle()
    {
        Initiate.Fade(sceneNameTitle, fadeColor, fadeSpeed);
    }
}
