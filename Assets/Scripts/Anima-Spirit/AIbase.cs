using UnityEngine;
using System.Collections;

[RequireComponent (typeof( NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CharacterController))]


public abstract class AIbase : MonoBehaviour {

	bool autoFire = false;
	bool isPossessed = false;

   public enum States
    {
        idle,
        walk,
        rotate,
        retreat,
        possessed,
        pursue
    };

    public States AIState = States.idle;
    float idleTime = 3.0f;
    bool AIReady = false;

    Transform spawnPoint;
    NavMeshAgent AIBody;
    CharacterController moveControl;
    Rigidbody rb;
    float distance;
	 
	protected abstract void ActivateAbility();
	protected abstract void PassiveAbility();
	// Use this for initialization
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").GetComponent<Transform>();
        AIBody = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

    }
	// Update is called once per frame
	void Update () {
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

	void Roam()
	{
		//logic for AI roaming
        switch (AIState)
        {
            case States.idle:
                //plays AI specific Animations
              //  AIState = States.walk;
                AIBody.Move(transform.forward * 100);
                print("hey");
                break;
            case States.walk:
                //walks forward for a set amount of time (random distance)
                gameObject.transform.localPosition += transform.forward*100;
                break;
            case States.rotate:
                //rotates for set amount of time (random angle)
                break;
            case States.retreat:
                //runs back to start
                break;
            case States.possessed:
                //logic for when possessed. Most probably disabling the charactercontroller of AI
                //with this, the functions below may need to obe placed in here.
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

	void OnMouseDown()
	{
		if(GameControl.spiritmode)
		{
			//possess logic
			isPossessed = true;

		}
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
