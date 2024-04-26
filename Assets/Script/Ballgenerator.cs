using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballgenerator : MonoBehaviour
{
    public GameObject BallPrefab;

    float time = 1.0f;
    float delta = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.time)
        {
            this.delta = 0;
            GameObject s = Instantiate(BallPrefab) as GameObject;
            int px = Random.Range(-5, 6);
            s.transform.position = new Vector2(px, 7);
        }
    }
}
