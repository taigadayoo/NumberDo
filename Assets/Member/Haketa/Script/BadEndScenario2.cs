using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadEndScenario2 : MonoBehaviour
{
    public Question question;
    [SerializeField]
    private CSVRerderBadEnd2 _csvrerder;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Text _name;
    [SerializeField]
    private GameObject _do;
    [SerializeField]
    private GameObject _black;
    [SerializeField] 
    private GameObject _white;
    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private GameObject _maecamera;
    [SerializeField]
    private GameObject _bgmobj;
    [SerializeField]
    private AudioSource _batan;
    [SerializeField]
    private AudioSource _brain;
    
    
    bool check = true;
    public int math = 0;
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
        if (check)
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
                    anim.SetBool("isbadend", false);
                }
                else if ("BadEnd" == question.move)
                {
                    //_black.SetActive(false);
                    //_bgmobj.SetActive(true);
                    //anim.SetBool("isusua_b", false);
                    //anim.SetBool("isbadend", true);
                }
                else if("Black" == question.move)
                {
                    check = false;
                    //ì|ÇÍÇÈSE
                    _batan.PlayOneShot(_batan.clip);
                    _black.SetActive(true);
                    await UniTask.Delay(TimeSpan.FromSeconds(3.0f));
                    _maecamera.SetActive(true);
                    _camera.SetActive(false);
                    _black.SetActive(false);
                    _bgmobj.SetActive(true);
                    anim.SetBool("isusua_b", false);
                    anim.SetBool("isbadend", true);
                    check = true;
               
                }
                else if ("Black2" == question.move)
                {
                    check = false;
                    //ì|ÇÍÇÈSE
                    _batan.PlayOneShot(_batan.clip);
                    _black.SetActive(true);
                    await UniTask.Delay(TimeSpan.FromSeconds(3.0f));
                    _maecamera.SetActive(true);
                    _camera.SetActive(false);
                    _bgmobj.SetActive(true);
                    _do.SetActive(false);
                    _black.SetActive(false);
                    _white.SetActive(true);
                    check = true;
                }
                else if ("Off2" == question.move)
                {
                    _brain.PlayOneShot(_brain.clip);
                    

                }
                else if ("End" == question.move)

                {
                    SampleSoundManager.Instance.StopBgm();

                    SampleSoundManager.Instance.PlayBgm(BgmType.BGM3);

                    await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

                    SceneManagement.Instance.OnMainGame();
                  
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