using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuSceneScript : MonoBehaviour {
	Transform currentMount;
	bool _trigger;
	bool _fade;
	float _t;
	float _t2;
	public CanvasGroup FadeObj;
	// Use this for initialization
	void Start () {
		currentMount = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (_trigger)
		{
			_t += Time.deltaTime/10;
			
		}
		if (_t > 1)
		{
			_t = 1;
			_trigger = false;
		}

		transform.position = Vector3.Lerp(transform.position, currentMount.position, _t);
		transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, _t);
		if (_fade)
		{
			_t2 += Time.deltaTime;
		}
		if(_t2>1)
		{
			_t2 = 1;
		}
		if (!_fade)
		{
			_t2 -= Time.deltaTime;
		}
		if (_t2 < 0)
		{
			_t2 = 0;
		}
		//if (_fade && _trigger)
		//{
		//	_t2 += Time.deltaTime;
		//	if (_t2 > 1)
		//	{
		//		_t2 = 1;
		//		_fade = false;
		//		_trigger = false;
		//	}
			
		//}
		//else if (!_fade && _trigger)
		//{
		//	_t2 -= Time.deltaTime;
		//	if (_t2 < 0)
		//	{
		//		_t2 = 0;
		//		_fade = true;
		//	}
		//}
		
		
		FadeObj.alpha = Mathf.Lerp(0,1,_t2);
	
	}
	public void SetMount(Transform mountPos)
	{
		_t = 0;
		currentMount = mountPos;
		_trigger = true;
	}
	public void Fade()
	{
		_fade = !_fade;
	}
}
