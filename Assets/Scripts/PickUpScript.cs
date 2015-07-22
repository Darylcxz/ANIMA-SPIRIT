﻿using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {


    public GameObject _attach;
    

    bool hasItem = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	
   
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Torch"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !hasItem)
            {
                other.gameObject.transform.parent = _attach.transform;
               // other.gameObject.transform.position = _attach.transform.position;
                hasItem = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && hasItem)
            {
                _attach.transform.GetChild(0).transform.parent = null;
                hasItem = false;
            }
        }
       
       
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (!hasItem)
        //    {
        //        if (other.gameObject.tag == "Torch")
        //        {
        //            other.gameObject.transform.parent = _attach.transform;
        //            other.gameObject.transform.position = _attach.transform.position;
        //            hasItem = true;

        //        }  
        //    }
        //}
    }
   
}