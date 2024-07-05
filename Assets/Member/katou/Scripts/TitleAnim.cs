using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class TitleAnim : MonoBehaviour
{
    private Animator anim;
    public GameObject panel;

    public GameObject Panelfade;   

    Image fadealpha;              

    private float alpha;           

    private bool fadeout;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        fadealpha = Panelfade.GetComponent<Image>();
        alpha = fadealpha.color.a;
    }

   public async UniTask TitleAnimation()
    {
        panel.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2));
        anim.SetBool("isTitle", true);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        FadeOut();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        anim.SetBool("isTitle2",true);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
    }

    void FadeOut()
    {
        alpha += 0.6f;
        fadealpha.color = new Color(0, 0, 0, alpha);
    }


}
