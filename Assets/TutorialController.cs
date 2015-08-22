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

	//public DialogueScript _dScript;

	bool ready;
	float _camSize = 4.0f;
	float _t;
	bool bTimer = true;
	float _temp;

	// Use this for initialization
	void Start () {
		
		//_charCam.SetActive(false);
		CamPanScript = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraPanning>();
		_dScript = GameObject.FindGameObjectWithTag("GulnazBody").GetComponent<DialogueScript>();
		_camChar.GetComponent<Camera>();
		_movieCam.GetComponent<Camera>();
		_camChar.enabled = false;
		
	
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
		if (DialogueScript._seqNum == 4 && DialogueScript.NPCname.Contains("desh"))
		{
			sequenceNum = 0;
		}
		
		if (bTimer)
		{
			_t += Time.deltaTime / 1.5f ;
			_temp = Mathf.Lerp(13.0f, _camSize, _t);
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
				Invoke("Wait", 5.0f);
				
				_movieCam.orthographicSize = _temp;//Mathf.Lerp(13.0f, _camSize, 0.1);
				bTimer = true;
				
				//_camChar.enabled = true;
				if (ready)
				{
					_camChar.enabled = true;

					ready = false;
				//	_t = 0;
					CancelInvoke();
				}
				break;
			case 1:
				_camChar.enabled = false;
				ready = false;
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
		}
	
	}
	void Wait()
	{
		ready = true;
	}
}
