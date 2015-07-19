using UnityEngine;
using System.Collections;

public class GulnazGrab : MonoBehaviour {

    public Transform attachpoint;
    private bool holding = false;

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "movable" && Input.GetButtonDown("Action") && holding == false)
        {
            other.gameObject.transform.SetParent(transform);
            holding = true;
        }
        else if(other.gameObject.tag == "movable" && Input.GetButtonDown("Action") && holding)
        {
            holding = false;
            print("unattach");
            other.gameObject.transform.parent = null;
        }

    }
	// Update is called once per frame
	void Update () {

        transform.position = attachpoint.transform.position;
	}

    
}
