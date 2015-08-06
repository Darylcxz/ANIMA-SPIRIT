﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof( NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CharacterController))]


public abstract class AIbase : MonoBehaviour {

	bool autoFire = false;
//	bool isPossessed = false;
    public bool canPosses = true;

    protected Transform player;
    protected NavMeshAgent agent;
    protected Rigidbody _rigidBody;

   public enum States
    {
        idle,
        walk,
        rotate,
        retreat,
        possessed,
        pursue
    };

    //states related stuff
    public States AIState = States.walk;
    bool ready = false;
    protected float waitTime = 3.0f;
    protected Vector3 origin;
    protected float distance;
    public float retreatDist;

    //movement stuff
    float vMove;
    float hMove;
    float speed = 5f; //player movement speed;
    public float AISpeed = 1f; // AI selfmove speed;

    
	 
	protected abstract void ActivateAbility();
	protected abstract void PassiveAbility();
   // protected abstract void AggroAbility();
	// Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        origin = gameObject.transform.position;

    }
	// Update is called once per frame
	protected void Update () {
        distance = Vector3.Distance(gameObject.transform.position, origin); //distance between you and origin
     
        Roam();
        PassiveAbility();		
        
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
                gameObject.transform.localPosition += (transform.forward/35)*AISpeed;
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
                
                agent.SetDestination(origin);
                if (distance < 10)
                {
                    agent.ResetPath();
                    AIState = States.idle;
                    ready = false;
                    CancelInvoke();
                }
                break;
            case States.possessed:
                //logic for when possessed. Most probably disabling the charactercontroller of AI
                //with this, the functions below may need to be placed in here.
                agent.ResetPath();
                ready = false;
                cameratarget.followTarget = gameObject;
                CheckInput();
                AIMove();
                if (GameControl.spiritmode == false)
                {
                    AIState = States.idle;
                    agent.ResetPath();
                    ready = false;
                    cameratarget.followTarget = GameObject.Find("Character");
                    CancelInvoke();
                }
                break;
            case States.pursue:
                //faces player and walks toward player, reserved for enemy behaviour
                ActivateAbility(); //Enemy Behaviour
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

        //if(Input.GetMouseButtonDown(1))
        //{
        //    isPossessed = false;
        //}

	}

    void WaitTimer()
    {
        ready = true;
    }

    protected virtual Vector3 OriginPos()
    {
        origin = gameObject.transform.position;
        return origin;
    }


    void OnMouseDown()
    {
        if (GameControl.spiritmode && canPosses)
        {
            //possess logic
            AIState = States.possessed;

        }
    }

    void AIMove()
    {
       // hMove = Input.GetAxis("Horizontal");
        //vMove = Input.GetAxis("Vertical");

       // Vector3 targetVelocity = new Vector3(hMove, 0, vMove);
		Vector3 targetVelocity = new Vector3(GamepadManager.h1, 0, GamepadManager.v1);

		targetVelocity.Normalize();
        targetVelocity *= speed;
        Vector3 velocity = _rigidBody.velocity;
        Vector3 vChange = targetVelocity - velocity;
        vChange = new Vector3(Mathf.Clamp(vChange.x, -speed, speed), 0, (Mathf.Clamp(vChange.z, -speed, speed)));
        transform.LookAt(transform.position + targetVelocity);
        _rigidBody.AddForce(vChange, ForceMode.VelocityChange);
    }

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