﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animTest : MonoBehaviour
{
    public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(anim.isPlaying);
    }
}
