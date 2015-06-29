using UnityEngine;
using System.Collections;

public class Turning : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Horizontal") < 0) {
			transform.eulerAngles = new Vector3 (transform.rotation.x, -90, transform.rotation.z);
		} else if (Input.GetAxis ("Horizontal") > 0) {
			transform.eulerAngles = new Vector3 (transform.rotation.x, 90, transform.rotation.z);
		}

	
	}
}
