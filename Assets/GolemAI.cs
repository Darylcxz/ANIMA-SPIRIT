using UnityEngine;
using System.Collections;

public class GolemAI : AIbase {


	Animator GolemController;
	float moveSpeed;

	// Use this for initialization
	protected override void Start()
	{
		//base.Start();
		_rigidBody = GetComponent<Rigidbody>();
		origin = gameObject.transform.position;
		GolemController = GetComponent<Animator>();
	}
	protected override void ActivateAbility()
	{
		//throw new System.NotImplementedException();
	}
	protected override void PassiveAbility()
	{
		//throw new System.NotImplementedException();
		moveSpeed = _rigidBody.velocity.magnitude;
		GolemController.SetFloat("moveSpeed", moveSpeed);
		Debug.Log(moveSpeed);
	}
}
