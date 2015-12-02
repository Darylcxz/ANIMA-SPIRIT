﻿using UnityEngine;
using System.Collections;

public class SpitFlowerAI : MonoBehaviour {
	GameObject player;
	GameObject target;
	public Rigidbody projectile;
	public Transform shootPoint;
	Rigidbody _rb;
	float timer;
	float distance;
	bool shoot;
	Vector3 targetSpeed;
	Vector3 futurePos;
	Vector3 direction;
	float iterations = 30f;
	public enum NeptoAI
	{
 		SHOOT,
		IDLE,
		DEATH
	}
	public NeptoAI NeptoState = NeptoAI.IDLE;
	Animator NeptoController;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		_rb = GetComponent<Rigidbody>();
		NeptoController = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	//	Debug.Log(timer);
		distance = Vector3.Distance(player.transform.position, transform.position);
		AILogic();
		timer += Time.deltaTime;
	}
	void LookAtPlayer()
	{
		target = player.gameObject;
		targetSpeed = target.GetComponent<Rigidbody>().velocity;
		futurePos = target.transform.position + (targetSpeed * (distance/iterations));
		direction = futurePos - transform.position;
		//direction.y = 0;
		Quaternion lookRot = Quaternion.LookRotation(direction);
		_rb.MoveRotation(lookRot);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2f * Time.deltaTime);
	}
	void AILogic()
	{
		switch (NeptoState)
		{
 			case NeptoAI.IDLE:
				NeptoController.SetBool("isAttacking", false);
				if (distance < 15 && timer > 0 && !NeptoController.GetBool("isAttacking"))
				{
					Debug.Log("IDLE > SHOOT");
					NeptoController.SetBool("isAttacking", true);
					shoot = false;
					timer = 0;
					NeptoState = NeptoAI.SHOOT;
				}
				break;
			case NeptoAI.SHOOT:
				NeptoController.SetBool("isAttacking", true);
				LookAtPlayer();
				if (timer > 2.4f && !shoot && NeptoController.GetBool("isAttacking"))
				{
					Fire();
					Debug.Log("Fire");
				}
				if ((timer >= 3.75f || distance > 15) && NeptoController.GetBool("isAttacking"))
				{
					Debug.Log("SHOOT > IDLE");
					NeptoController.SetBool("isAttacking", false);
					NeptoState = NeptoAI.IDLE;
				}
				break;
			case NeptoAI.DEATH:

				break;

		}
	}
	void Fire()
	{
		Rigidbody projectileClone = Instantiate(projectile, shootPoint.position, transform.rotation) as Rigidbody;
		projectileClone.velocity = transform.forward *20;
		shoot = true;
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Player")
		{
 			//play Death2 anim
			//NeptoController.SetBool("isHit", true);
		}
		if (col.collider.tag == "Ball")
		{
 			//play Death1 anim
			NeptoController.SetBool("isDead", true);
			_rb.isKinematic = false;
		}
	}
}