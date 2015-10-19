﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	//public GameObject _charCam;
	//public GameObject _camMovie;
	public Image _rtText;

	public Camera _camChar;
	public Camera _movieCam;

	public CameraPanning CamPanScript;
	public DialogueScript _dScript;
	public List<GameObject> mountList = new List<GameObject>();
	public MovementController _moveScript;
	public DeshTutorial _dTutScript;
	public GameObject Serik;

	public int sequenceNum;

	public Animator deshControl;

	public Transform _target;
	public NavMeshAgent _desh;

	//public DialogueScript _dScript;

	bool ready;
	float _camSize = 4.0f;
	float _t;
	bool bTimer = true;
	float _temp;

	bool slowT;

	// Use this for initialization
	void Start () {
		
		//_charCam.SetActive(false);
		CamPanScript = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraPanning>();
		_dScript = GameObject.FindGameObjectWithTag("GulnazBody").GetComponent<DialogueScript>();
		_camChar.GetComponent<Camera>();
		_movieCam.GetComponent<Camera>();
		_camChar.enabled = false;
		deshControl = GameObject.FindGameObjectWithTag("desh").GetComponent<Animator>();
		_desh = GameObject.FindGameObjectWithTag("desh").GetComponent<NavMeshAgent>();
		_moveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
		_dTutScript = GameObject.FindGameObjectWithTag("desh").GetComponent<DeshTutorial>();

	
	}
	
	// Update is called once per frame
	 void Update () {
		 Debug.Log(DialogueScript._seqNum + "up");
		
		if (Input.GetKeyDown(KeyCode.T))
		{
			sequenceNum++;
		}
		CamPanScript.SetMount(mountList[sequenceNum].transform);
		if (DialogueScript._seqNum == 3 && DialogueScript.NPCname.Contains("desh"))
		{
			sequenceNum = 1;
			CamPanScript.SetMount(mountList[sequenceNum].transform);
			Debug.Log("dwed");
		}
		
		
		if (bTimer)
		{
			_t += Time.deltaTime / 1.5f ;
			_temp = Mathf.Lerp(13.0f, _camSize, _t);
		}
		if (slowT)
		{
			_t += Time.deltaTime / 2f;
			Time.timeScale = Mathf.Lerp(1f, 0f, _t);
		}
		if (_t > 1)
		{
			_t = 1;
			bTimer = false;
		}
		
		switch (sequenceNum)
		{
 			case 0:
				//Zoom when enter
				Invoke("Wait", 5.0f);
				
				_movieCam.orthographicSize = _temp;//Mathf.Lerp(13.0f, _camSize, 0.1);
				bTimer = true;
				
				//_camChar.enabled = true;
				if (ready)
				{
					_camChar.enabled = true;
					ready = false;
					bTimer = false;
				//	_t = 0;
					CancelInvoke();
				}
				break;
			case 1:
				//zoom to desh
				_camChar.enabled = false;
				ready = false;
				if (DialogueScript._seqNum == 4 && DialogueScript.NPCname.Contains("desh"))
				{
					sequenceNum = 2;
				}
				break;
			case 2:
				//back to player
				if (DialogueScript.NPCname.Contains("Shake"))
				{
					sequenceNum = 3;
					_t = 0;
					ready = false; 
				}
				break;
			case 3:
				slowT = true;
				if (DialogueScript._seqNum == 1)
				{
					sequenceNum = 4;
				}
				_desh.SetDestination(_target.position);
				//float waittime = 2.0f;
				
				//AHH 
				//slow-mo
				//Invoke("Wait", waittime);
	
				//if (ready)
				//{
				//	ready = false;
				//	sequenceNum = 4;
				//	Debug.Log("move to unfreeze");
				//	CancelInvoke();
				//}
				break;
			case 4:
				//it's coming for you!
				//player roll
				//show graphic
				if (DialogueScript._seqNum == 0)
				{
					_rtText.enabled = true;
 
				}
				

				if (Input.GetKeyDown(KeyCode.LeftShift) && _moveScript.bForcedMove == false)
				{
					slowT = false;
					_t = 0;
					Time.timeScale = 1;
					sequenceNum = 5;
					
				}
				break;
			case 5:
				//back to player + roll
				//close graphic
				_rtText.enabled = false;
				Debug.Log(_dTutScript.deshDead);
				if (_dTutScript.deshDead == true)
				{
					DialogueScript.NPCname = "defeated1-1";
					string textData = _dScript.dialogue.text;
					_dScript.ParseDialogue(textData);
					sequenceNum = 6;
				}
				
				break;
			case 6:
				//player kills desh
				if (DialogueScript._seqNum == 1 && DialogueScript.NPCname.Contains("shoes"))
				{
					Serik.SetActive(false);
				}
				Debug.Log("dwedwfew");
				//NPCname = gameObject.name;
			    
				
				break;
			case 7:
				break;

		}
	
	}
	void Wait()
	{
		ready = true;
	}
}
