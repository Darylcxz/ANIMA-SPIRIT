using UnityEngine;
using System.Collections;

public class TongueRenderscript : MonoBehaviour {
    private LineRenderer tongue;
    private GameObject tongueStart;
    public static Rigidbody lt;
    private float dist;
    public static Vector3 origin;
    public static Rigidbody handle;
    

	// Use this for initialization
	void Start () {
        tongue = this.gameObject.GetComponent<LineRenderer>();
        tongueStart = GameObject.Find("tongueStart");
        lt = this.gameObject.GetComponent<Rigidbody>();
        lt.constraints = RigidbodyConstraints.FreezeAll;
	
	}
	
	// Update is called once per frame
	void Update () {

        origin = tongueStart.transform.position;

        tongue.SetPosition(0, transform.position);
        tongue.SetPosition(1, tongueStart.transform.position);
        dist = Vector3.Distance(transform.position, tongueStart.transform.position);
        Vector3 backDir = tongueStart.transform.position - transform.position;
        if(dist > 5)
        {
            transform.position = tongueStart.transform.position;
            lt.constraints = RigidbodyConstraints.FreezeAll;
        }

       
	
	}

    public static void shootTongue(Vector3 shootdir)
    {
        lt.constraints = RigidbodyConstraints.None;
        lt.AddForce(shootdir, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Handle")
        {
            MirrorLizardAI.attachedtoHandle = true;
            lt.velocity = Vector3.zero;
            lt.constraints = RigidbodyConstraints.FreezeAll;
            handle = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    public static void Move()
    {
        Vector3 backDir = lt.gameObject.transform.position - origin;
        Vector3 moveDir = backDir;
        handle.AddForce(-moveDir, ForceMode.Impulse);
        lt.transform.position = origin;
    }
}
