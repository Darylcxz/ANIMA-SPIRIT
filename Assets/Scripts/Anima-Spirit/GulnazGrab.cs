using UnityEngine;
using System.Collections;

public class GulnazGrab : MonoBehaviour {

    private RaycastHit hit;
    public static bool struggling = false;
    public Transform attachpoint;
    private Vector3 control = new Vector3(0, 0.3f, 0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if(Physics.Raycast(transform.position + control, transform.forward, out hit, 1))
        {
            Debug.DrawRay(transform.position + control, transform.forward * 1);
            if(Input.GetKeyDown("e") && hit.collider.tag == "movable")
            {
                if(struggling == false)
                {
                    struggling = true;
                    hit.collider.transform.SetParent(attachpoint);
                    print("parent la knn");
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
