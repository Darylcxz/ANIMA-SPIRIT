using UnityEngine;
using System.Collections;

public class NeptoProjectile : MonoBehaviour {
	Rigidbody _rb;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Player")
		{
			col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(10, transform.position, 3, 0, ForceMode.Impulse);
			Destroy(gameObject, 0.5f);
		}
	}
	void BackToSender(int test)
	{ 
		//go back to the plant that sent it.
		if (test == 1)
		{
 			//return it back to sender :v
		}
	}
}
