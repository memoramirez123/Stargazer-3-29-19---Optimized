
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMaker : MonoBehaviour
{
    public ParticleSystem rain;
    //public ParticleSystem particleSystemComponent = null;
    //public float gravity = 2f;

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            rain.Emit(2);
        }

    }
}