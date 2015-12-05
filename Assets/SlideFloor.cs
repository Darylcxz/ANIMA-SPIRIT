using UnityEngine;
using System.Collections;

public class SlideFloor : MonoBehaviour {
	Vector3 vel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//void OnTriggerEnter(Collider col)
	//{
	//	if (col.tag == "Player")
	//	{
	//		vel = col.GetComponent<Rigidbody>().velocity.normalized;
	//	}
	//}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			//col.GetComponent<MovementController>().bForcedMove = true;
			//col.GetComponent<Rigidbody>().AddForce(vel * 50, ForceMode.Impulse);
			if (col.gameObject.transform.localEulerAngles.y > 315 || col.gameObject.transform.localEulerAngles.y < 45)
			{
				vel = new Vector3(0, 0, 0);
			}
			else if (col.gameObject.transform.localEulerAngles.y > 45 || col.gameObject.transform.localEulerAngles.y < 135)
			{
				vel = new Vector3(0, 90, 0);
			}
			else if (col.gameObject.transform.localEulerAngles.y > 135 || col.gameObject.transform.localEulerAngles.y < 225)
			{
				vel = new Vector3(0, 180, 0);
			}
			else if (col.gameObject.transform.localEulerAngles.y > 225 || col.gameObject.transform.localEulerAngles.y < 315)
			{
				vel = new Vector3(0, 270, 0); 
			}
		}
	}
}
