using UnityEngine;
using System.Collections;

public class GulnazGrab : MonoBehaviour {

    private RaycastHit hit;
    public static bool struggling;
    public Transform attachpoint;
	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {

        if(Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            if(Input.GetKeyDown("e") && hit.collider.tag == "movable")
            {
                if(struggling == false)
                {
                    struggling = true;
                    hit.collider.transform.SetParent(attachpoint);
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
