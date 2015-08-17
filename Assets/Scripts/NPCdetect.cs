using UnityEngine;
using System.Collections;

public class NPCdetect : MonoBehaviour {
    private GameObject character;
    private bool facechara = false;

	// Use this for initialization
	void Start () {

        character = GameObject.Find("Character");
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 targetDir = character.transform.position - transform.position;
        float dist = Vector3.Distance(transform.position,transform.position);
        if(dist < 3)
        {
            DialogueScript.NPCname = gameObject.name;
            DialogueScript.cantalk = true;
            if(Input.GetButtonDown("Action"))
            {
                facechara = true;
            }

            if(facechara == true)
            {
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 2 * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }
	}
}
