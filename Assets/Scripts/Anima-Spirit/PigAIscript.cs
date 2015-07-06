using UnityEngine;
using System.Collections;

public class PigAIscript : AIbase {

	public ParticleSystem kaboom;
	public ParticleSystem bigBoom;
	public Light glow;
	private bool glows = true;

	// Use this for initialization
	void Start () {

		print("start");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	protected override void ActivateAbility()
	{
		if(Input.GetButtonDown("Action"))
		{
			kaboom.Play();
		}
	}

	protected override void PassiveAbility()
	{
		if(glows)
			glow.intensity += 0.1f;

		else if(!glows)
			glow.intensity -= 0.1f;

		if(glow.intensity >= 3)
			glows = false;
		else if(glow.intensity <= 2)
			glows = true;
	}
}
