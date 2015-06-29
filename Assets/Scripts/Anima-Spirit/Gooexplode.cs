using UnityEngine;
using System.Collections;

public class Gooexplode : MonoBehaviour {

	public ParticleSystem bigboom;
	public static bool onGoo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "Pig" && Input.GetButtonDown("Action")) {
			Instantiate(bigboom,transform.position,Quaternion.identity);
			Destroy(gameObject);
			onGoo = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "Pig") {
			onGoo = false;
		}
	}

}
