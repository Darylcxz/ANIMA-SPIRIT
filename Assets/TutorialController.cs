using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialController : MonoBehaviour {

	//public GameObject _charCam;
	//public GameObject _camMovie;
	public Camera _camChar;
	public Camera _movieCam;

	public CameraPanning CamPanScript;
	public List<GameObject> mountList = new List<GameObject>();
	public int sequenceNum;


	bool ready;
	float _camSize = 3.0f;
	float _t;
	bool bTimer = true;
	float _temp;

	// Use this for initialization
	void Start () {
		//_charCam.SetActive(false);
		CamPanScript = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraPanning>();
		_camChar.GetComponent<Camera>();
		_movieCam.GetComponent<Camera>();
		_camChar.enabled = false;
		
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.T))
		{
			sequenceNum++;
		}
		CamPanScript.SetMount(mountList[sequenceNum].transform);
		
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
		if (sequenceNum > 4)
		{
			sequenceNum = 0;
		}
		switch (sequenceNum)
		{
 			case 0:
				Invoke("Wait", 5.0f);
				_movieCam.orthographicSize = _temp;//Mathf.Lerp(13.0f, _camSize, 0.1);
				
				//_camChar.enabled = true;
				if (ready)
				{
					_camChar.enabled = true;
					ready = false;
 
				}
				break;
			case 1:
				_camChar.enabled = false;
				
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
