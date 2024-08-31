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
                //�V�[���J��
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