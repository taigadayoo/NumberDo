using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballgenerator : MonoBehaviour
{
    public GameObject BallPrefab;
    public GameObject popup;
    public TimeCounter timeCounter;


    float time = 0.5f;
    float delta = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ƒ‰ƒ“ƒ_ƒ€¶¬
        this.delta += Time.deltaTime;
        if (this.delta > this.time)
        {
            //1.3•b‚É‚È‚Á‚½‚Æ‚«
            if (1.3 <= timeCounter.countdown)
            {
                this.delta = 0;
                GameObject s = Instantiate(BallPrefab) as GameObject;
                int px = Random.Range(-5, 5);
                s.transform.position = new Vector2(px, 3);
            }
        }

    }
}
