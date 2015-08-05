using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	private Rigidbody2D jm;
	private Vector2 moveDirection = Vector3.zero;
	public float movespeed;
	public float jumpspeed;
	private bool onGround = true;
	private Vector2 temp1;
	private Vector2 temp2;


	// Use this for initialization
	void Start () {
		jm = GetComponent<Rigidbody2D> ();

		temp1 = transform.localScale;
		temp2 = transform.localScale;
		temp2.x *= -1;
	}

//	void OnCollisionStay2D (Collision2D other)
//	{
//		if (other.gameObject.tag == "floor") {
//			onGround = true;
//		}
//		
//	}
//
//	void OnCollisionExit2D (Collision2D other)
//	{
//		if (other.gameObject.tag == "floor") {
//			onGround = false;
//		}
//	}


	// Update is called once per frame
	void Update () {

		if (onGround == true) {
			moveDirection = new Vector2 (Input.GetAxisRaw ("Horizontal"),0);
			moveDirection *= movespeed;

			if (Input.GetButtonDown ("Jump")) {
				moveDirection.y = jumpspeed;
				//onGround = false;
			}
			jm.AddForce (moveDirection, ForceMode2D.Impulse);
		}

		if (Input.GetAxis ("Horizontal") < 0) {
			transform.localScale = temp2;
		} else if (Input.GetAxis ("Horizontal") > 0) {
			transform.localScale = temp1;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "red") {
			moveDirection.y = 70;
			jm.AddForce (moveDirection, ForceMode2D.Impulse);
		}

		if (other.gameObject.tag == "blue") {
		}

		if (other.gameObject.tag == "yellow") {
			jm.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "blue") {
			jm.AddForce(Vector2.right * 10,ForceMode2D.Impulse);
		}

		if (other.gameObject.tag == "purple") {
			Vector2 suckaim = other.transform.position - transform.position;
			jm.AddForce(suckaim * 20, ForceMode2D.Force);
			if(Input.GetKeyDown("space"))
			{
				jm.AddForce(Vector2.up * -70, ForceMode2D.Impulse);
			}

		}
	}
}
