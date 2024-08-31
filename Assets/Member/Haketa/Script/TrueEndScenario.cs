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