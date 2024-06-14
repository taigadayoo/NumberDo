using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballgenerator : MonoBehaviour
{
    public GameObject BallPrefab;
    public GameObject popup;
    public TimeCounter timeCounter;
    [SerializeField] private Transform[] positions;

    float time = 0.5f;
    float delta = 0;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //ƒ‰ƒ“ƒ_ƒ€¶¬
        this.delta += Time.deltaTime;
        if (this.delta > this.time)
        {
            //1.3•b‚É‚È‚Á‚½‚Æ‚«
            if (1 <= timeCounter.countdown)
            {
                this.delta = 0;
                int randomIndex = Random.Range(0, positions.Length);
                Vector2 randomPosition = positions[randomIndex].position;

                Instantiate(BallPrefab, randomPosition, Quaternion.identity);
             
            }
        }

    }
}
