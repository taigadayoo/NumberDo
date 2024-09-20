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
    private AudioSource _audioSource;

    public int math = 0;
    private bool _chack = true;
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
        if (_chack == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ReadQuestion();
                ++math;
                if ("usually_blood" == question.move)
                {
                    anim.SetBool("isusua_b", true);
                    anim.SetBool("istrueend", false);
                }
                else if ("TrueEnd" == question.move)
                {
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
                    _panel.SetActive(false);
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
        question = _csvrerder.GetQuestion();
        _text.text = question.bun;
        _name.text = question.name;
    }
}