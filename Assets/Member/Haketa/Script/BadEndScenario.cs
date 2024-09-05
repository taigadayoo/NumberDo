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
    public GameObject _backBlack;
    bool _chack = true;
    public int math = 0;
    [SerializeField]
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        ReadQuestion();
    }

    // Update is called once per frame
    async void Update()
    {
        if (_chack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ReadQuestion();
                ++math;
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
                    SampleSoundManager.Instance.PlayBgm(BgmType.BGM3);
                    SceneManagement.Instance.OnMainGame();
                }
                else if("Black" ==  question.move)
                {
                    _chack = false;
                    //”š”­SE
                    audioSource.PlayOneShot(audioSource.clip);
                    _black.SetActive(true);
                    _do.SetActive(false);
                    await UniTask.Delay(TimeSpan.FromSeconds(4.0f));
                    _black.SetActive(false);
                    _backBlack.SetActive(true);
                    //_red.SetActive(true);
                    _chack = true;

                }
            }
        }
    }

    private void ReadQuestion()
    {
        question = _csvrerder.GetQuestion();
        _text.text = question.bun;
        _name.text = question.name;
    }
}