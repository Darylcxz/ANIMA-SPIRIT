using UnityEngine;
using System.Collections;

public abstract class AIbase : MonoBehaviour {

	bool autoFire = false;
	bool isPossessed = false;
	 
	protected abstract void ActivateAbility();
	protected abstract void PassiveAbility();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PassiveAbility ();
		if (!isPossessed) {
			Roam ();
		}
		else
		{
			CheckInput ();

		}
		gameObject.GetComponent<CharacterController>().enabled = isPossessed;




	
	}

	void Roam()
	{
		//logic for AI roaming
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


}
