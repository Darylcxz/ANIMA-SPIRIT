using UnityEngine;
using System.Collections;

public class JellyCamerafollow : MonoBehaviour {
	private GameObject followTarget;

	// Use this for initialization
	void Start () {
	
		followTarget = GameObject.Find ("Jellyman");

	}
	
	// Update is called once per frame
	void Update () {

		transform.position = followTarget.transform.position;
	
	}
}
