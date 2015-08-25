using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	private GameObject character;
	public static bool spiritmode = false;
	public Texture bolangu;
	public Texture spiritjotai;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

		//if (Input.GetMouseButtonDown (1)) 

		if(GamepadManager.buttonYDown || Input.GetMouseButtonDown(1))
		{

			possessModeToggle();
		}
	
	}

	void possessModeToggle() {

		if (spiritmode == false) {

			spiritmode = true;

			print ("possess mode activated");

		} else if (spiritmode == true) {

			print ("possess mode deactivated");
		//	charactermovement.isBeingControlled = true;
			Camerafollow.targetUnit = GameObject.Find("Character");
			spiritmode = false;

		}

	}

	void OnGUI()
	{
		if(spiritmode == true)
			GUI.DrawTexture(new Rect(0, 0, 1600, 900), spiritjotai, ScaleMode.StretchToFill, true, 10.0F);


		if (KeyItemSpin.holdingItem == true) {
			GUI.DrawTexture(new Rect(1400, 760, 150, 100), bolangu, ScaleMode.StretchToFill, true, 10.0F);
		}
	}
	
}
