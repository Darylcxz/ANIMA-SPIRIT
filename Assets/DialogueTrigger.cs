using UnityEngine;
using System.Collections;

public class DialogueTrigger : DialogueScript {

//	public MovementController _mScript2;

	//public override void Start()
	//{
	//	base.Start();
	//	//_mScript2 = GetComponent<MovementController>();
	//}

	// Use this for initialization
	//public override void Start () {
	//	base.Start();
	
	//}
	
	// Update is called once per frame
	//public override void Update () {
	//	base.Update();
	//}
	void OnTriggerEnter(Collider _col)
	{
		if (_col.tag == "Player")
		{
			NPCname = gameObject.name;
			string textData = dialogue.text;
			ParseDialogue(textData);
		}
	}
}
