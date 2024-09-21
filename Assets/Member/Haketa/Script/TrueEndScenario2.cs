using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static Unity.Collections.AllocatorManager;

public class TrueEndScenario2 : MonoBehaviour
{
    public Question question;
    [SerializeField]
    private CSVRerderTrueEnd2 _csvrerder;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Text _name;
    [SerializeField]
    private GameObject _textbox;
    [SerializeField]
    private VideoPlayer _videoPlayer;

    public GameObject panel;
    public int math = 0;
    private bool chack = true;
    [SerializeField]
    public Animator anim;

    private bool isTextDisplaying = false;
    private string fullText;
    private Coroutine displayCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        //anim = GetComponent<Animator>();
        ReadQuestion();
        _videoPlayer.gameObject.SetActive(false);
    }

    // Update is called once per frame
   async void Update()
    {
        if (chack && Input.GetMouseButtonDown(0))
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
            if ("StopBGM" == question.move)
            {
                SampleSoundManager.Instance.StopBgm();
            }
            //else if ("TrueEnd" == question.move)
            //{
            //    anim.SetBool("isusua_b", false);
            //    anim.SetBool("istrueend", true);
            //}
            if ("PlayVideo" == question.move)
            {
             
                chack = false;
                _textbox.SetActive(false);
                PlayVideo();
                await UniTask.Delay(TimeSpan.FromSeconds(7.0f));
                chack = false;
                panel.SetActive(true);
                SceneManagement.Instance.OnTrueEnd3();
            }
            else if ("End" == question.move)
            {
               
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
        _videoPlayer.gameObject.SetActive(true);
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);
        chack = true;
    }
}