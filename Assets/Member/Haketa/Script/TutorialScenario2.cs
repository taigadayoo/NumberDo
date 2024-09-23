using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static Unity.Collections.AllocatorManager;
using Cysharp.Threading.Tasks;
using System;
public class TutorialScenario2 : MonoBehaviour
{
    public GameObject doModel;
    public Question question;
    [SerializeField]
    private CSVRerderTutorial2 _csvrerder;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Text _name;
    private bool chack = true;
    public GameObject panel;
    public int math = 0;
    [SerializeField]
    public Animator anim;
    [SerializeField]
    private VideoPlayer _videoPlayer;
    [SerializeField]
    private GameObject _textbox;
    private bool isTextDisplaying = false;
    private string fullText;
    private Coroutine displayCoroutine;
    public GameObject bikkuri;
    // Start is called before the first frame update
    void Start()
    {
        doModel.SetActive(false);
        //anim = GetComponent<Animator>();
        ReadQuestion();
        
    }

    // Update is called once per frame
   async void Update()
    {
        if (Input.GetMouseButtonDown(0) && chack)
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
            //    anim.SetBool("isthink_b", false);
            //}
            if ("suffer_blood" == question.move)
            {
                anim.SetBool("isthink_b", false);
                anim.SetBool("issuff_b", true);
            }
           else if ("Black" == question.move)
            {
                SampleSoundManager.Instance.StopBgm();
                panel.SetActive(true);
                SampleSoundManager.Instance.PlaySe(SeType.SE8);

                _textbox.SetActive(false);


                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                chack = false;
                PlayVideo();

                await UniTask.Delay(TimeSpan.FromSeconds(4f));
                panel.SetActive(false);
                bikkuri.SetActive(true);

                await UniTask.Delay(TimeSpan.FromSeconds(3f));
                doModel.SetActive(true);
                bikkuri.SetActive(false);
                chack = true;
                _textbox.SetActive(true);
                SampleSoundManager.Instance.PlayBgm(BgmType.BGM3);
            }
            else if ("understanding_blood" == question.move)
            {
                anim.SetBool("issuff_b", false);
                anim.SetBool("isunder_b", true);
            }
            else if ("think_blood" == question.move)
            {
                anim.SetBool("isthink_b", true);
                anim.SetBool("isunder_b", false);
            }
            else if ("End" == question.move)
            {
                SceneManagement.Instance.OnMainGame();
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