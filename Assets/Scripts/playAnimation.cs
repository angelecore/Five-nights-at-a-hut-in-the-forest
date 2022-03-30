using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnimation : MonoBehaviour { 
    public bool play;
    ParticleSystem[] dust;

    // Start is called before the first frame update
    void Start()
    {
        dust = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            dust[0].Play();
            dust[1].Play();
            play = false;
        }
    }
}
