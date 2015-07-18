using UnityEngine;
using System.Collections;

public class Droptrigger : MonoBehaviour {
	// Use this for initialization
    public static bool shouldFall;
	void Start () {
	
	}
	
	// Update is called once per frame
    void OnTriggerEnter()
    {
        shouldFall = true;
    }
}
