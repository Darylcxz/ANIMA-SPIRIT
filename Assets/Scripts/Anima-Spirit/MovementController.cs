using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    enum States
    {
        idle,
        move,
        stab,
        swing,
        jump,
        roll,
    };

    States charStates = States.idle;

    bool roll;
    bool attack;
    bool jump;
    float hMove;
    float vMove;

    bool isGrounded;
    bool ready;


	// Use this for initialization
	void Start () {
        
        roll = Input.GetKeyDown(KeyCode.LeftShift);
        attack = Input.GetMouseButtonDown(0);
        jump = Input.GetKeyDown(KeyCode.Space);
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");
	
	}
	
	// Update is called once per frame
	void Update () {


        switch (charStates)
        {
            case States.idle:
                if (jump)
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
                }
                break;
            case States.move:
                break;
            case States.jump:
                break;
            case States.roll:
                break;
            case States.stab:
                break;
            case States.swing:
                break;
        }
	}


}
