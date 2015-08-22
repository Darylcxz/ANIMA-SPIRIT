using UnityEngine;
using System.Collections;

public class CameraPanning : MonoBehaviour {

	public Transform currentMount;
	public float speed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, currentMount.position, speed);
		transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speed);
	}
	void SetMount(Transform newMount)
	{
		currentMount = newMount;
	}

}
