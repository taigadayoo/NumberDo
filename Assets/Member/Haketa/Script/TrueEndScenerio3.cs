using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static Unity.Collections.AllocatorManager;

public class TrueEndScenario3 : MonoBehaviour
{
    public Question question;
    [SerializeField]
    private CSVRerderTrueEnd3 _csvrerder;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Text _name;
    [SerializeField]
    private VideoPlayer _videoPlayer;
    [SerializeField]
    private GameObject _textbox;
    public int math = 0;
    [SerializeField]
    public Animator anim;
    public GameObject panel;
    private bool isTextDisplaying = false;
    private string fullText;
    private Coroutine displayCoroutine;

    private bool skip = false;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        ReadQuestion();
    }

    // Update is called once per frame
   async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && skip)
        {
            SceneManagement.Instance.OnTitle();
        }
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
            //if ("usually_blood" == question.move)
            //{
            //    anim.SetBool("isusua_b", true);
            //    anim.SetBool("istrueend", false);
            //}
            //else if ("TrueEnd" == question.move)
            //{
            //    anim.SetBool("isusua_b", false);
            //    anim.SetBool("istrueend", true);
            //}
         if ("End" == question.move)
            {
                _textbox.SetActive(false);
                await UniTask.Delay(TimeSpan.FromSeconds(2.0f));

                PlayVideo();
              
                await UniTask.Delay(TimeSpan.FromSeconds(63.0f));

                SceneManagement.Instance.OnTitle();
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
    private void PlayVideo()
    {
        
        skip = true;
        _videoPlayer.gameObject.SetActive(true);
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);
 
    }
}