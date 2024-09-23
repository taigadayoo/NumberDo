using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrueEndScenario : MonoBehaviour
{
    public Question question;
    [SerializeField]
    private CSVRerderTrueEnd _csvrerder;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Text _name;

    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private GameObject _do;
    [SerializeField]
    private GameObject _textbox;
    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private GameObject _maecamera;
    [SerializeField]
    private GameObject _bgmobj;
    [SerializeField]
    private AudioSource _audioSource;

    public int math = 0;
    private bool _chack = true;
    [SerializeField]
    public Animator anim;

    private bool isTextDisplaying = false;
    private string fullText;
    private Coroutine displayCoroutine;
    // Start is called before the first frame update
    void Start()
    {
       
        //anim = GetComponent<Animator>();
        ReadQuestion();
    }

    // Update is called once per frame
    async void Update()
    {
        if (_chack == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isTextDisplaying)
                {
                    StopCoroutine(displayCoroutine);
                    _text.text = fullText;
                    isTextDisplaying = false;
                }
                else
                {
                    ++math;
                    ReadQuestion();
                }
                if ("usually_blood" == question.move)
                {
                    anim.SetBool("isusua_b", true);
                    anim.SetBool("istrueend", false);
                }
                else if ("TrueEnd" == question.move)
                {
                    _panel.SetActive(false);
                    anim.SetBool("isusua_b", false);
                    anim.SetBool("istrueend", true);
                }
                else if ("Black" == question.move)
                {
                    _chack = false;
                    _panel.SetActive(true);
                    //SEä‘Ç…çáÇ¡Çƒñ≥Ç©Ç¡ÇΩÇÁÇ±Ç±Ç…èeê∫
                    _audioSource.PlayOneShot(_audioSource.clip);
                    await UniTask.Delay(TimeSpan.FromSeconds(3.0f));
                    _maecamera.SetActive(true);
                    _camera.SetActive(false);
                    //_panel.SetActive(false);
                    _panel.SetActive(false);
                    SampleSoundManager.Instance.PlayBgm(BgmType.BGM5);
                    anim.SetBool("isusua_b", false);
                    anim.SetBool("istrueend", true);
                    _chack = true;
                }
                else if("Off" == question.move)
                {
                    _do.SetActive(false);
                }
                else if ("End" == question.move)
                {
                    SceneManagement.Instance.OnTrueEnd();
                }
            }
        }
    }

    private void ReadQuestion()
    {
        if (math < _csvrerder.Questions.Count)
        {
            question = _csvrerder.GetQuestion();
            fullText = question.bun;
            _name.text = question.name;

            displayCoroutine = StartCoroutine(DisplayText(fullText));
        }

    }

    private IEnumerator DisplayText(string text)
    {
        _text.text = "";
        isTextDisplaying = true;
        foreach (char letter in text)
        {
            _text.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        isTextDisplaying = false;
    }
}