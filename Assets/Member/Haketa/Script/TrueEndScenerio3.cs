using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrueEndScenario3 : MonoBehaviour
{
    public Question question;
    [SerializeField]
    private CSVRerderTrueEnd3 _csvrerder;
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
                //ÉVÅ[ÉìëJà⁄
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