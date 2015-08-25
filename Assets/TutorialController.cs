using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialController : MonoBehaviour {

	//public GameObject _charCam;
	//public GameObject _camMovie;
	public Camera _camChar;
	public Camera _movieCam;

	public CameraPanning CamPanScript;
	public DialogueScript _dScript;
	public List<GameObject> mountList = new List<GameObject>();
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
		if (sequenceNum > 5)
		{
			sequenceNum = 0;
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
				float waittime = 2.0f;
				
				//AHH it's coming for you!
				Invoke("Wait", waittime);
	
				if (ready)
				{
					ready = false;
					sequenceNum = 4;
					Debug.Log("move to unfreeze");
					CancelInvoke();
				}
				break;
			case 4:
				//back to player
				if (Input.GetKeyDown(KeyCode.LeftShift))
				{
					slowT = false;
					_t = 0;
					Time.timeScale = 1;
				}
				break;
			case 5:
				//slow-mo shenanigans
				break;
			case 6:
				//player kills desh
				break;

		}
	
	}
	void Wait()
	{
		ready = true;
	}
}
