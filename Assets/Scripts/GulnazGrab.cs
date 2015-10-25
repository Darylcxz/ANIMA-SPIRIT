﻿using UnityEngine;
using System.Collections;

public class GulnazGrab : MonoBehaviour {
    //private bool touching = false;
    public static bool holding = false;
    private Vector3 center;
    private Vector3 side1;
    private Vector3 side2;
    private RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {

        center = transform.position + new Vector3(0, 0.5f, 0);
        side1 = center + new Vector3(0.2f, 0, 0);
        side2 = center + new Vector3(-0.2f, 0, 0);
		Debug.DrawRay(center, transform.forward );
		Debug.DrawRay(side1, transform.forward );
		Debug.DrawRay(side2, transform.forward );

        if (Physics.Raycast(center, transform.forward, out hit, 0.5f) && Input.GetButtonDown("Action") || Physics.Raycast(side1, transform.forward, out hit, 0.5f) && Input.GetButtonDown("Action") || Physics.Raycast(side2, transform.forward, out hit, 0.5f) && Input.GetButtonDown("Action"))
        {
			Debug.Log(hit.collider.tag);
			if(!holding && hit.collider.tag == "movable")
            {
                hit.collider.transform.SetParent(transform);
                holding = true;
            }
            else if(holding)
            {
                hit.collider.transform.parent = null;
                holding = false;
            }
			if (hit.collider.tag == "lever")
			{
 				hit.collider.gameObject.GetComponent<Animator>().SetBool("bLever",true);
				GameObject.Find("gate").GetComponent<Animator>().SetBool("bLever", true);
			}
        }

	}

    
}
