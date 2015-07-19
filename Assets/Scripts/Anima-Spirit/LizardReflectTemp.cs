using UnityEngine;
using System.Collections;

public class LizardReflectTemp : MonoBehaviour {
    private LineRenderer lightray;
    private RaycastHit hit1;
    private RaycastHit hit2;
    public int rayint;
    public static Ray[] rayz = new Ray[3];
    private GameObject originpt;
	// Use this for initialization
	void Start () {
        lightray = GetComponent<LineRenderer>();
        originpt = GameObject.Find("lightsource2");
        rayz[0] = new Ray(originpt.transform.position, originpt.transform.forward);
	}
	
	// Update is called once per frame
	void Update () {

        if(rayint == 0)
        {
            if (Physics.Raycast(rayz[0], out hit1, 200))
            {
                if (hit1.collider.name == "Mirror1")
                {
                    lightray.SetPosition(0, originpt.transform.position);
                    lightray.SetPosition(1, hit1.point);
                    rayz[1] = new Ray(hit1.point, Vector3.Reflect(rayz[0].direction, hit1.normal));
                }
            }
        }

        else if(rayint == 1)
        {
            if (Physics.Raycast(rayz[1], out hit2, 200))
            {
                Debug.DrawLine(hit1.point, hit2.point);
                lightray.SetPosition(0, hit1.point);
                lightray.SetPosition(1, hit2.point);
            }

        }
	}
}
