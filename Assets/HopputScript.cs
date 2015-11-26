using UnityEngine;
using System.Collections;

public class HopputScript : AIbase {

	float AggroDist = 10f;
	float jumpRange = 15f;
	float timerThing = 0.0f;
	float playerDist;
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
	//void Start () {
	
	//}
	
	// Update is called once per frame
	//void Update () {
	
	//}

	//void Start()
	//{
	//	//base.Start();
	//	agent = GetComponent<NavMeshAgent>();
	//	_rigidBody = GetComponent<Rigidbody>();
	//	origin = gameObject.transform.position;
	//}
	protected override void Roam()
	{
		base.Roam();
		playerDist = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (distance < AggroDist)
		{
			AIState = States.pursue;
		}
	}
	protected override void ActivateAbility()
	{
		//explosion.Play();
		currentTargetPosition = player.transform.position;
		//MoveTowardsTarget();
	//	Debug.Log("chasing" + "" + player.transform.position);

		switch (hopState)
		{
 			case HopputState.IDLE:
				if (timerThing > 3)
				{
					timerThing = 0;
					hopState = HopputState.MOVE;
				}
				break;
			case HopputState.MOVE:
				MoveTowardsTarget();
				if (playerDist < jumpRange)
				{
					hopState = HopputState.SHAKE;
					timerThing = 0;
				}
				break;
			case HopputState.SHAKE:
				//shake anim
				if (timerThing > 2)
				{
					hopState = HopputState.ATTACK;
					timerThing = 0;
				}
				break;
			case HopputState.ATTACK:
				//attack anim
				MoveTowardsTarget();
				if (playerDist < 2)
				{
					timerThing = 0;
					hopState = HopputState.IDLE;
				}
				break;
			case HopputState.RECOIL:
				//hurt anim
				if (timerThing > 5)
				{
					hopState = HopputState.MOVE;
					timerThing = 0;
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
}
