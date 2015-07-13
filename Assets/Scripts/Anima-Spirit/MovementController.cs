using UnityEngine;
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

    bool roll;
    bool attack;
    bool jump;
    bool possess;
    float hMove;
    float vMove;

    int attackMode = 0; //1: Stab, 2: swing

    float groundDist;
    float waitTime = 0.5f;
    bool ready = false;
    bool isRolling = false;

    public float speed = 15.0f;
    public float smoothDamp = 15.0f;

    Rigidbody _rigidBody;

    Vector3 groundPos;
    Vector3 playerPos;


	// Use this for initialization
	void Start () {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        groundDist = gameObject.GetComponent<Collider>().bounds.center.y;
        _anim = gameObject.GetComponent<Animator>();
        
        
        
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckInput();
        Debug.Log(attackMode + "" + ready);
        _anim.SetFloat("speed", _rigidBody.velocity.magnitude); // changes anim speed value to make it play move anim
        _anim.SetInteger("attack", attackMode); //1: stab, 2:swing
        _anim.SetBool("isRolling", isRolling);//change param to be the same as bool isRolling
        switch (charStates)
        {
            case States.idle:
            //check if player is grounded
                attackMode = 0;
                ready = false;
                isRolling = false;
                if (vMove != 0f || hMove != 0f)
                {
                    charStates = States.move;
                   
                }
                if (jump && isGrounded())
                {
                    charStates = States.jump;
                }
                if (attack)
                {
                    charStates = States.stab;
                }
                if (roll)
                {
                    charStates = States.roll;
                    isRolling = true;
                }
                break;
            case States.move:
                RotatingLogic(hMove, vMove);
                MovementLogic();
                attackMode = 0;
                if (vMove == 0 && hMove ==0)
                {
                    charStates = States.idle;
                }
                if (jump && isGrounded())
                {
                    charStates = States.jump;
                }
                break;
            case States.jump:
                _rigidBody.AddForce(Vector3.up*5,ForceMode.Impulse);
                if (!isGrounded())
                {
                    charStates = States.idle;
                }           
                break;
            case States.possess:
                break;
            case States.roll:
                
                if (!isRolling)
                {
                    charStates = States.idle;
                }
                break;
            case States.stab:
                attackMode = 1;
                if(attack)
                {                  
                    charStates = States.swing;
                   // ready = false;           
                }
                if (ready)
                {
                    charStates = States.idle;
                }
                break;
            case States.swing:
                attackMode = 2;
                if (ready)
                {
                    charStates = States.idle;          
                }          
                if (attack)
                {
                    charStates = States.stab;
                  //  ready = false;
                }
                break;
        }
	}

    void CheckInput()
    {
        roll = Input.GetKeyDown(KeyCode.LeftShift);
        attack = Input.GetMouseButtonDown(0);
        jump = Input.GetKeyDown(KeyCode.Space);
        possess = Input.GetMouseButtonDown(1);
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");
    }
    void MovementLogic()
    {
        Vector3 targetVelocity = new Vector3(hMove, 0, vMove);
        targetVelocity.Normalize();
        targetVelocity *= speed;
        Vector3 velocity = _rigidBody.velocity;
        Vector3 vChange = targetVelocity - velocity;
        vChange = new Vector3(Mathf.Clamp(vChange.x,-speed, speed), 0, (Mathf.Clamp(vChange.z, -speed, speed)));
        _rigidBody.AddForce(vChange, ForceMode.VelocityChange);
        
    }
    void RotatingLogic(float h, float v)
    {
        Vector3 targetDir = new Vector3(h, 0, v);
        Quaternion targetRot = Quaternion.LookRotation(targetDir,Vector3.up);
        Quaternion newRot = Quaternion.Lerp(_rigidBody.rotation, targetRot, smoothDamp * Time.deltaTime);
        _rigidBody.MoveRotation(newRot);
    }
    public void attackEnd()
    {
        ready = true;
        Debug.Log("Attack ended");
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
