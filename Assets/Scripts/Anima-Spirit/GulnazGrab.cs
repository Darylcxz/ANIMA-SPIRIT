using UnityEngine;
using System.Collections;

public class GulnazGrab : MonoBehaviour {

    private RaycastHit hit;
    public static bool struggling;
	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {

        if(Physics.Raycast(transform.position, transform.forward, out hit, 50))
        {
            if(Input.GetButtonDown("Action") && hit.collider.tag == "movable")
            {
                if(struggling == false)
                {
                    struggling = true;
                    hit.collider.transform.SetParent(transform);
                }

                else if(struggling == true)
                {
                    struggling = false;
                    hit.collider.transform.parent = null;
                }
            }
        }
	
	}
}
