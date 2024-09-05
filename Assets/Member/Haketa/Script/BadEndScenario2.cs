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
    bool check = true;
    public int math = 0;
    [SerializeField]
    public Animator anim;
    SceneManagement sceneManagement;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        ReadQuestion();
     sceneManagement =    FindObjectOfType<SceneManagement>();
    }

    // Update is called once per frame
    async void Update()
    {
        if (check)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ReadQuestion();
                ++math;
                if ("usually_blood" == question.move)
                {
                    anim.SetBool("isusua_b", true);
                    anim.SetBool("isbadend", false);
                }
                else if ("BadEnd" == question.move)
                {
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
                    SampleSoundManager.Instance.PlayBgm(BgmType.BGM3);
                    SceneManagement.Instance.OnMainGame();
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