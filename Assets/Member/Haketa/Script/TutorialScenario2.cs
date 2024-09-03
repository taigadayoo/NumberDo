using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScenario2 : MonoBehaviour
{
    public Question question;
    [SerializeField]
    private CSVRerderTutorial2 _csvrerder;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Text _name;

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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ReadQuestion();
            ++math;
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
                //ÉVÅ[ÉìëJà⁄
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