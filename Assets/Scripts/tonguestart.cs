﻿using UnityEngine;
using System.Collections;

public class tonguestart : MonoBehaviour {
    public GameObject followthatshit;
	// Use this for initialization
	void Start () {

        
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = followthatshit.transform.position;
	
	}
}
