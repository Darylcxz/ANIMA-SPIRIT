﻿using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	
	public enum States
    {
        idle,
        move,
        stab,
        swing,
        jump,
        roll,
        possess,
    };

   public States charStates = States.idle;
   Animator _anim;

    //input stuff
   public bool bKeyboard = true;

    //bool roll;
    float roll;
    bool attack;
    bool jump;
    bool possess;
    float hMove;
    float vMove;


    float vMoveRight;
    float hMoveRight;

    int attackMode = 0; //1: Stab, 2: swing
	float attackSpeed = 1;

//    float groundDist;
//    float waitTime = 0.5f;
    bool ready = false;
    bool isRolling = false;
    

    public float speed = 15.0f;
    public float jumpForce = 5.0f;
    public float smoothDamp = 15.0f;

    Rigidbody _rigidBody;
//    Collider _collider;

    Vector3 groundPos;
    Vector3 playerPos;


	// Use this for initialization
	void Start () {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
     //   groundDist = gameObject.GetComponent<Collider>().bounds.center.y;
     //   _collider = gameObject.GetComponent<Collider>();
        _anim = gameObject.GetComponent<Animator>();

        
        
        
	
	}
    void Update()
    {
        CheckInput();
        //        Debug.Log(_rigidBody.velocity.magnitude);
        _anim.SetFloat("speed", _rigidBody.velocity.magnitude); // changes anim speed value to make it play move anim
        _anim.SetInteger("attack", attackMode); //1: stab, 2:swing
        _anim.SetBool("isRolling", isRolling);//change param to be the same as bool isRolling
		Debug.Log(attackMode);
		_anim.speed = attackSpeed;
		
		if (attackSpeed > 3)
		{
			attackSpeed = 3;
		}
		if (attackSpeed < 1)
		{
			attackSpeed = 1;
		}
    }
	
	// Update is called once per frame
	void FixedUpdate () {
       

        if (possess && ready == false && isRolling == false && isGrounded())
        {
            charStates = States.possess;
        }

        switch (charStates)
        {
            case States.idle:
            //check if player is grounded
                attackMode = 0;
              //	  ready = false;
                isRolling = false;
				attackSpeed = 1;
                if (vMove != 0f || hMove != 0f)
				//if(GamepadManager.v1 !=0 || GamepadManager.h1 !=0)
                {
                    charStates = States.move;     
                }
                if (jump && isGrounded())
                {
                    charStates = States.jump;
                }
                if (attack && GameControl.spiritmode == false)
                {
                    charStates = States.stab;
					attackMode = 1;
                }
                if (roll!=0)
                {
                    charStates = States.roll;
                    isRolling = true;
                }
                break;
            case States.move:
                RotatingLogic(hMove, vMove);
                MovementLogic(hMove,vMove);
				//RotatingLogic(GamepadManager.h1, GamepadManager.v1);
				//MovementLogic(GamepadManager.h1, GamepadManager.v1);
                attackMode = 0;
                if (vMove == 0 && hMove ==0)
				//if (GamepadManager.v1 == 0 && GamepadManager.h1 ==0)
                {
                    charStates = States.idle;
                }
                if (jump && isGrounded())
                {
                    charStates = States.jump;
                }
                break;
            case States.jump:
                _rigidBody.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
                if (!isGrounded())
                {
                    charStates = States.idle;
                }           
                break;
            case States.possess:
                MovementLogic(hMoveRight, vMoveRight);
                RotatingLogic(hMoveRight, vMoveRight);
				//MovementLogic(GamepadManager.h2, GamepadManager.v2);
				//RotatingLogic(GamepadManager.h2, GamepadManager.v2);
                if (GameControl.spiritmode == false)
                {
                    charStates = States.idle;

                    Debug.Log("possass");
                }
                break;
            case States.roll:
               // _rigidBody.AddForce(transform.forward/1.5f,ForceMode.Impulse);
               // _rigidBody.AddRelativeForce(transform.forward / 0.9f, ForceMode.Impulse);
               // _rigidBody.AddForceAtPosition(transform.forward *5, transform.localPosition);
                //_rigidBody.velocity += transform.forward/3f;
                gameObject.transform.localPosition +=( transform.forward*2*Time.deltaTime);
                if (!isRolling)
                {
                    charStates = States.idle;
                }
                break;
            case States.stab:
				attackSpeed+=Time.deltaTime;
             //   attackMode = 1;
                if(attack && attackMode == 1 && !ready)
                {                  
                    charStates = States.swing;
                 //   ready = false;
					attackMode = 2;
                }
                else if (ready)
                {
                    charStates = States.idle;
                }
                break;
            case States.swing:
				attackSpeed+=Time.deltaTime;
               // attackMode = 2;
				if (attack && attackMode == 2 && !ready)
				{
					charStates = States.stab;
					//ready = false;
					attackMode = 1;
				}
                else if (ready)
                {
                    charStates = States.idle;          
                }          
                
                break;
        }
	}

    void CheckInput()
    {
       // roll = Input.GetKeyDown(KeyCode.LeftShift);
		if (bKeyboard)
		{
			hMove = Input.GetAxis("Horizontal");
			vMove = Input.GetAxis("Vertical");
			//roll = Input.GetKeyDown(KeyCode.LeftShift);
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				roll = 1;
			}
			if (Input.GetKeyUp(KeyCode.LeftShift))
			{
 				roll =0;
			}
			attack = Input.GetMouseButtonDown(0);
			jump = Input.GetKeyDown(KeyCode.Space);
			possess = Input.GetMouseButtonDown(1);
		}
		else if (!bKeyboard)
		{
			hMove = GamepadManager.h1;
			vMove = GamepadManager.v1;
			roll = GamepadManager.triggerR;
			attack = GamepadManager.buttonX;
			jump = GamepadManager.buttonA;
			possess = GamepadManager.buttonY;
		}
		

        //attack = Input.GetMouseButtonDown(0);
		
		// jump = Input.GetKeyDown(KeyCode.Space);
		
       // possess = Input.GetMouseButtonDown(1);
		
       

       
    }
    void MovementLogic(float horizontal, float vertical)
    {
        if(GameControl.spiritmode == false)
        {
            Vector3 targetVelocity = new Vector3(horizontal, 0, vertical);
            targetVelocity.Normalize();
            targetVelocity *= speed;
            Vector3 velocity = _rigidBody.velocity;
            Vector3 vChange = targetVelocity - velocity;
            vChange = new Vector3(Mathf.Clamp(vChange.x, -speed, speed), 0, (Mathf.Clamp(vChange.z, -speed, speed)));
            _rigidBody.AddForce(vChange, ForceMode.VelocityChange);
        }
        
        
    }
    void RotatingLogic(float h, float v)
    {
        if (GameControl.spiritmode == false && GulnazGrab.holding == false)
        {
            Vector3 targetDir = new Vector3(h, 0, v).normalized;
			
            Quaternion targetRot = Quaternion.LookRotation(targetDir, Vector3.up);
            Quaternion newRot = Quaternion.Lerp(_rigidBody.rotation, targetRot, smoothDamp * Time.deltaTime);
            _rigidBody.MoveRotation(newRot);
        }
    }
    public void attackEnd()
    {
        ready = true;
		attackMode = 0;
      //  Debug.Log("Attack ended");
    }
    public void attackStart()
    {
        ready = false;
        Debug.Log("Attack started");
    }
    bool isGrounded()
    {      
        return Physics.Raycast(transform.position, -Vector3.up, 0.5f);
    }
    public void rollEnd()
    {
        isRolling = false;
    }
}