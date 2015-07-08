using UnityEngine;
using System.Collections;

public class EnemyChase : AIbase {
	enum State {Idle, Chase, Death, Return};
	State myStates;
	private NavMeshAgent agent;
	private GameObject target;
	private bool chasing;
	public Transform spawnpoint;
	public float range;
	// Use this for initialization
	void Start () {

		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.Find ("Character");
	}

	// Update is called once per frame
	void Update () {

		float distance = Vector3.Distance(target.transform.position, transform.position);
		if (distance <= range)
			myStates = State.Chase;
		else if (distance > range) {
			myStates = State.Idle;
		} 
		else if (distance > range && chasing == true) {
			myStates = State.Return;
		}

		switch (myStates) {
		case State.Idle:
			agent.Stop();
			break;

		case State.Chase:
			agent.SetDestination(target.transform.position);
			chasing = true;
			agent.Resume();
			break;

		case State.Return:
			agent.SetDestination(spawnpoint.position);
			agent.Resume();
			break;

		case State.Death:
			print("dead");
			break;
		}

	
	}

	protected override void ActivateAbility()
	{

	}

	protected override void PassiveAbility()
	{
		
	}


}
