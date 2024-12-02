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

    SampleSoundManager soundManager;

    private bool fadeout;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SampleSoundManager>();
        anim = gameObject.GetComponent<Animator>();
        fadealpha = Panelfade.GetComponent<Image>();
        alpha = fadealpha.color.a;
    }

   public async UniTask TitleAnimation()
    {
        panel.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.05));
        anim.SetBool("isTitle", true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        soundManager.PlaySe(SeType.SE6);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        FadeOut();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        anim.SetBool("isTitle2",true);

    }

    void FadeOut()
    {
        alpha += 1f;
        fadealpha.color = new Color(0, 0, 0, alpha);
    }


}
