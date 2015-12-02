using UnityEngine;
using System.Collections;

public class TongueRenderscript : MonoBehaviour {
    private LineRenderer tongue;
    private GameObject tongueStart;
    public static Rigidbody lt;
    

	// Use this for initialization
	void Start () {

        tongue = this.gameObject.GetComponent<LineRenderer>();
        tongueStart = GameObject.Find("tongueStart");
        lt = this.gameObject.GetComponent<Rigidbody>();
        
	
	}
	
	// Update is called once per frame
	void Update () {

        tongue.SetPosition(0, transform.position);
        tongue.SetPosition(1, tongueStart.transform.position);
        
	
	}

    public static void shootTongue(Vector3 shootdir)
    {
        lt.velocity = (shootdir * 10);
        print("shoooooooot");
        print(shootdir);
    }
}
