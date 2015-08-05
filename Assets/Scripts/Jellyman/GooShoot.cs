using UnityEngine;
using System.Collections;

public class GooShoot : MonoBehaviour {
	private ParticleSystem cannon;
	public ParticleSystem cannon2;
	public ParticleSystem cannon3;
	public ParticleSystem cannon4;
	public Camera cam;
	public Rigidbody2D bullet;
	public float bulletforce;
	// Use this for initialization
	void Start () {

		cannon = GetComponent<ParticleSystem> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			cannon.Play();
		}

		if (Input.GetButtonUp ("Fire1")) {
			cannon.Stop();
		}
		if (Input.GetButtonDown ("Fire2")) {
			cannon2.Play();
		}
		
		if (Input.GetButtonUp ("Fire2")) {
			cannon2.Stop();
		}
		if (Input.GetButtonDown ("Fire3")) {
			cannon3.Play();
		}
		
		if (Input.GetButtonUp ("Fire3")) {
			cannon3.Stop();
		}

		if (Input.GetButtonDown ("Fire4")) {
			cannon4.Play();
		}
		
		if (Input.GetButtonUp ("Fire4")) {
			cannon4.Stop();
		}



	}
}
