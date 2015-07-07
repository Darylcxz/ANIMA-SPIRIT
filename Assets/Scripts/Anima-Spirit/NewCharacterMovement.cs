using UnityEngine;
using System.Collections;

public class NewCharacterMovement : MonoBehaviour {

	private Vector3 moveDirection = Vector3.zero;
	public float movespeed;
	public float jumpforce;
	public float gravity;
	public static bool isControlling;
	private CharacterController ctrllr;
	private Animator myAnim;

	// Use this for initialization
	void Start () {

		isControlling = true;
		ctrllr = GetComponent<CharacterController>() as CharacterController;
		myAnim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if (isControlling == true) {
			if (ctrllr.isGrounded) {
				moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
				ctrllr.transform.LookAt(transform.position + moveDirection);
				moveDirection *= movespeed;
				myAnim.SetInteger("movespeed",1);
				
				if(Input.GetButtonDown("Jump"))
					moveDirection.y = jumpforce;
			}
			moveDirection.y -= gravity * Time.deltaTime;
			ctrllr.Move (moveDirection * Time.deltaTime);
		}

	
	}
}