using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadEndScenario : MonoBehaviour
{
    public Question question;
    [SerializeField]
    private CSVRerderBadEnd _csvrerder;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Text _name;
    [SerializeField]
    private GameObject _black;
    [SerializeField]
    private GameObject _red;
    [SerializeField]
    private GameObject _do;
    [SerializeField]
    private AudioSource audioSource;


    bool _chack = true;
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
        if (_chack && Input.GetMouseButtonDown(0))
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
            //if ("understanding" == question.move)
            //{
            //    anim.SetBool("isunder", true);
            //    anim.SetBool("isusua", false);
            //}
            //else if ("think" == question.move)
            //{
            //    anim.SetBool("isunder", false);
            //    anim.SetBool("isthink", true);
            //}
            //else if ("usually" == question.move)
            //{
            //    anim.SetBool("isthink", false);
            //    anim.SetBool("isusua", true);
            //}
            if ("End" == question.move)
            {
                SceneManagement.Instance.OnMainGame();
                SampleSoundManager.Instance.PlayBgm(BgmType.BGM3);
            }
            else if ("Black" == question.move)
            {
                _chack = false;
                //”š”­SE
                audioSource.PlayOneShot(audioSource.clip);
                _black.SetActive(true);
                _do.SetActive(false);
                await UniTask.Delay(TimeSpan.FromSeconds(5.0f));
                _black.SetActive(false);
                _red.SetActive(true);
                _chack = true;

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