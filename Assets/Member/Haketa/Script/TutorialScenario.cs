using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScenario: MonoBehaviour
{
    public Question question;
    [SerializeField]
    private CSVRerder _csvrerder;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Text _name;

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
        SampleSoundManager.Instance.PlayBgm(BgmType.BGM2);
    }

    // Update is called once per frame
    void Update()
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
            if ("understanding" == question.move)
            {
                anim.SetBool("isunder", true);
                anim.SetBool("isusua", false);
            }
            else if ("think" == question.move)
            {
                anim.SetBool("isunder", false);
                anim.SetBool("isthink", true);
            }
            else if ("usually" == question.move)
            {
                anim.SetBool("isthink", false);
                anim.SetBool("isusua", true);
            }
            else if ("End" == question.move)
            {
                SceneManagement.Instance.OnGame();
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