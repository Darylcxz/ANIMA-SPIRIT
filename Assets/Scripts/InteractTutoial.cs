﻿using UnityEngine;
using System.Collections;

public class InteractTutoial : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            VillageDialogue.interactOn = true;
            Destroy(gameObject);
        }
    }
}
