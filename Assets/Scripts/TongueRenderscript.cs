using UnityEngine;
using System.Collections;

public class TongueRenderscript : MonoBehaviour {
    private LineRenderer tongue;
    private GameObject tongueStart;
    public static Rigidbody lt;
    private float dist;
    private bool detached;
    

	// Use this for initialization
	void Start () {

        tongue = this.gameObject.GetComponent<LineRenderer>();
        tongueStart = GameObject.Find("tongueStart");
        lt = this.gameObject.GetComponent<Rigidbody>();
        lt.constraints = RigidbodyConstraints.FreezeAll;
	
	}
	
	// Update is called once per frame
	void Update () {

        tongue.SetPosition(0, transform.position);
        tongue.SetPosition(1, tongueStart.transform.position);
        dist = Vector3.Distance(transform.position, tongueStart.transform.position);
        Vector3 backDir = tongueStart.transform.position - transform.position;
        if(dist > 5)
        {
            lt.AddForce(backDir, ForceMode.Impulse);
        }

        if(detached)
        {

        }
	
	}

    public static void shootTongue(Vector3 shootdir)
    {
        lt.constraints = RigidbodyConstraints.None;
        lt.AddForce(shootdir, ForceMode.Impulse);
        print("shoooooooot");
        print(shootdir);
    }
}
