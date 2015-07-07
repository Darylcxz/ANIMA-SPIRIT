﻿using UnityEngine;
using System.Collections;

//[RequireComponent (typeof( NavMeshAgent))]
//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CharacterController))]


public abstract class AIbase : MonoBehaviour {

	bool autoFire = false;
	bool isPossessed = false;

    Transform player;
    NavMeshAgent Agent;

   public enum States
    {
        idle,
        walk,
        rotate,
        retreat,
        possessed,
        pursue
    };

    public States AIState = States.walk;
    bool ready = false;
    float waitTime = 3.0f;
    Vector3 origin;
    float distance;
   public float retreatDist = 50.0f;

    
	 
	protected abstract void ActivateAbility();
	protected abstract void PassiveAbility();
	// Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Agent = GetComponent<NavMeshAgent>();
        origin = gameObject.transform.position;

    }
	// Update is called once per frame
	protected void Update () {
        distance = Vector3.Distance(gameObject.transform.position, origin); //distance between you and origin
        Roam();
		PassiveAbility ();
        
        //if (!isPossessed) {
        //    Roam ();
        //}
        //else
        //{
        //    CheckInput ();
        //}
			
	}

	protected virtual void Roam()
	{
		//logic for AI roaming
        switch (AIState)
        {
            case States.idle:
                //plays AI specific Animations
                Invoke("WaitTimer", waitTime);





                if (ready)
                {
                    AIState = States.walk;
                    ready = false;
                    CancelInvoke();
                }
                break;
            case States.walk:
                //walks forward for a set amount of time (random distance)
                Invoke("WaitTimer", waitTime*(Random.Range(0.5f,2f)));
                gameObject.transform.localPosition += transform.forward/5;
                if(distance > retreatDist)
                {
                    AIState = States.retreat;
                    ready = false;
                    CancelInvoke();
                }
                else if (ready)
                {
                    AIState = States.rotate;
                    ready = false;
                    CancelInvoke();
                }
                break;
            case States.rotate:
                //rotates for set amount of time (random angle)
                Invoke("WaitTimer", waitTime*(Random.Range(0.1f,1.0f)));
                gameObject.transform.eulerAngles += new Vector3(0, Mathf.Ceil(Random.Range(-2,1)), 0);
                if (ready)
                {
                    AIState = States.walk;
                    ready = false;
                    CancelInvoke();
                }
                break;
            case States.retreat:
                //runs back to start
                Agent.SetDestination(origin);
                if (distance < 10)
                {
                    Agent.ResetPath();
                    AIState = States.idle;
                    ready = false;
                    CancelInvoke();
                }
                break;
            case States.possessed:
                //logic for when possessed. Most probably disabling the charactercontroller of AI
                //with this, the functions below may need to obe placed in here.
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActivateAbility();
                }
                break;
            case States.pursue:
                //faces player and walks toward player. additional feature, may be discarded. Good to have.
                break;


        }

	}

	void CheckInput()
	{
		bool PressedE;
		
		if(autoFire)
		{
			PressedE =  Input.GetKey(KeyCode.E);
		}
		else
		{
			PressedE = Input.GetKeyDown(KeyCode.E);
		}
		if(PressedE)
		{
			ActivateAbility();
		}

		if(Input.GetMouseButtonDown(1))
		{
			isPossessed = false;
		}

	}

    void WaitTimer()
    {
        ready = true;
    }

    //void OnMouseDown()
    //{
    //    if(GameControl.spiritmode)
    //    {
    //        //possess logic
    //        isPossessed = true;

    //    }
    //}

    //void GoodMovement()
    //{
    //    Vector3 targetVelocity = new Vector3(h1, 0, v1);
    //    targetVelocity.Normalize();
    //    targetVelocity *= speed;
    //    Vector3 v = _rigidbody.velocity;
    //    Vector3 velocityChange = targetVelocity - v;
    //    velocityChange = new Vector3(Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange), 0, Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange));

    //    _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

    //}


}
