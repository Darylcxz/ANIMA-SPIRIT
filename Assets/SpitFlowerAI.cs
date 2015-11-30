using UnityEngine;
using System.Collections;

public class SpitFlowerAI : MonoBehaviour {
	GameObject player;
	Rigidbody _rb;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		LookAtPlayer();
	}
	void LookAtPlayer()
	{
		Vector3 targetPos = player.transform.position - transform.position;
		Quaternion lookRot = Quaternion.LookRotation(targetPos);
		_rb.MoveRotation(lookRot);
	}
}
