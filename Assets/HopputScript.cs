using UnityEngine;
using System.Collections;

public class HopputScript : AIbase {

	float AggroDist = 10f;
	float jumpRange = 15f;
	float timerThing = 0.0f;
	float playerDist;
	Animator HopputAnim;
	GameObject childObj;
	public enum HopputState
	{
		IDLE,
		MOVE,
		SHAKE,
		ATTACK,
		RECOIL

	};
	public HopputState hopState = HopputState.MOVE;
	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		childObj = gameObject.transform.GetChild(0).gameObject;
		HopputAnim = gameObject.GetComponentInParent<Animator>();
	}
	
	protected override void Roam()
	{
		base.Roam();
		playerDist = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (distance < AggroDist)
		{
			AIState = States.pursue;
		}
		Debug.DrawRay(childObj.transform.position, -Vector3.up, Color.black);
	}
	protected override void ActivateAbility()
	{
		
		currentTargetPosition = player.transform.position;


		switch (hopState)
		{
 			case HopputState.IDLE:
				if (timerThing > 3)
				{
					timerThing = 0;
					hopState = HopputState.MOVE;
					HopputAnim.SetInteger("AnimState", 1);
				}
				break;
			case HopputState.MOVE:
				MoveTowardsTarget();
				//maybe make it roam instead?
				if (playerDist < jumpRange)
				{
					hopState = HopputState.SHAKE;
					timerThing = 0;
					HopputAnim.SetInteger("AnimState", 2);
				}
				break;
			case HopputState.SHAKE:
				//shake anim
				if (timerThing > 3)
				{
					hopState = HopputState.ATTACK;
					timerThing = 0;
					HopputAnim.SetInteger("AnimState", 4);
				}
				break;
			case HopputState.ATTACK:
				//attack anim
				AISpeed = 5f;
				MoveTowardsTarget();
				if (isGrounded())
				{
					timerThing = 0;
					hopState = HopputState.IDLE;
					AISpeed = 1f;
					HopputAnim.SetInteger("AnimState", 0);
				}
				//if (playerDist < 2)
				//{
				//	timerThing = 0;
				//	//instantiate shockwave
				//	hopState = HopputState.IDLE;
				//	AISpeed = 1f;
				//	HopputAnim.SetInteger("AnimState", 0);
				//}
				//else if (playerDist > jumpRange)
				//{
				//	AISpeed = 1f;
				//	timerThing = 0;
				//	hopState = HopputState.IDLE;
				//	HopputAnim.SetInteger("AnimState", 0);
				//}
				break;
			case HopputState.RECOIL:
				//hurt anim
				if (timerThing > 5)
				{
					hopState = HopputState.MOVE;
					timerThing = 0;
					HopputAnim.SetInteger("AnimState", 1);
				}
				break;

		}

	}
	protected override void PassiveAbility()
	{
		//  throw new System.NotImplementedException();
		timerThing += Time.deltaTime;
	}

	void OnCollisionEnter(Collision col)
	{
 		if(col.collider.tag == "dagger")
		{
			health--;
			hopState = HopputState.RECOIL;
		}
	}
	bool isGrounded()
	{
		return Physics.Raycast(childObj.transform.position, -Vector3.up, 0.5f);
	}
}
