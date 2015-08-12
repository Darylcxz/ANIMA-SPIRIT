using UnityEngine;
using System.Collections;

public class DeshAI : AIbase {


    public float AggroDist;
    public float shake;
	bool ready2 = false;
	bool hit;

	public enum AttackPattern
	{
		SHAKE,
		DASH,
		IDLE,
		RECOIL,
	};
	public AttackPattern attackStates = AttackPattern.SHAKE;

//	CameraShake ShakeScript;
	

	// Use this for initialization
	void Start () {
      
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();
        origin = gameObject.transform.position;
        canPosses = false;
		ready2 = false;

//		ShakeScript = GetComponent<CameraShake>();
		
	}
    protected override void Roam()
    {
        base.Roam();
//        Debug.Log(distance);
        if (distance < AggroDist)
        {     
          //  ready2 = false; //basic reset. Should probably make a reset function in AI base
            AIState = States.pursue; //calls the activate ability bit in AIBase;
        }
       
    }

    protected override void ActivateAbility()
    {
        //do camera shake stuff here;
       // Debug.Log("sad");
        //Vibrate(5f);
		switch (attackStates)
		{
 			case AttackPattern.SHAKE:
				Invoke("WaitTime", 3f);
				Debug.Log("Vibrato");
				//Util.Shake(gameObject, 5f);
				//Vibrate(5f);
				Vibrate();
				
				
				
				if (ready2)
				{
					attackStates = AttackPattern.DASH;
					ready2 = false;
					CancelInvoke();
				}
				break;
			case AttackPattern.DASH:
				agent.SetDestination(player.position);
				agent.speed = 7;
				agent.autoBraking = false;
				if (hit)
				{
					attackStates = AttackPattern.IDLE;
					agent.ResetPath();
					agent.speed = 3.5f;
					agent.autoBraking = true;
					CancelInvoke();
					hit = false;
					ready2 = false;
				}
				break;
			case AttackPattern.IDLE:
				Invoke("WaitTime", 2f);
				//Debug.Log("idle");
				if (ready2)
				{
					attackStates = AttackPattern.SHAKE;
					ready2 = false;
					CancelInvoke();
				}
				break;
			case AttackPattern.RECOIL:
				break;
		}
		
        //agent.SetDestination(player.position);
    }
    protected override void PassiveAbility()
    {
       // throw new System.NotImplementedException();
    }
	

	void Vibrate()
	{
		Vector3 _origin = gameObject.transform.localPosition;
		float shakeAmt = 0.3f;
		//float minusFactor = 1.0f;
		//shake = _f;

		
		gameObject.transform.localPosition = transform.localPosition + Random.insideUnitSphere * shakeAmt;
	//	Debug.Log("swqsd");
		
		//shake -= Time.deltaTime * minusFactor;
	}
	void WaitTime()
	{
		ready2 = true;
	}
	void OnCollisionEnter(Collision _col)
	{
		if (_col.collider.tag == "Player")
		{
			hit = true;
		}
	}
}
