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
    private AudioSource _batan;
    [SerializeField]
    private AudioSource _brain;
    [SerializeField]
    private AudioSource _bgm;
    bool check = true;
    public int math = 0;
    [SerializeField]
    public Animator anim;

    private bool isTextDisplaying = false;
    private string fullText;
    private Coroutine displayCoroutine;

    private bool batan = false;
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
                    if (!batan)
                    {
                        _bgm.Play();
                        batan = true;
                    }
                    anim.SetBool("isusua_b", false);
                    anim.SetBool("isbadend", true);
                }
                else if("Black" == question.move)
                {
                    check = false;
                    //ì|ÇÍÇÈSE
                    _batan.PlayOneShot(_batan.clip);
                    _black.SetActive(true);
                    await UniTask.Delay(TimeSpan.FromSeconds(3.0f));
                    _black.SetActive(false);
                    check = true;
               
                }
                else if ("Off2" == question.move)
                {
                   _brain.PlayOneShot(_brain.clip);
                    _white.SetActive(true);
                    _do.SetActive(false);
                }
                else if ("End" == question.move)

                {
                    SceneManagement.Instance.OnMainGame();
                    SampleSoundManager.Instance.PlayBgm(BgmType.BGM3);
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