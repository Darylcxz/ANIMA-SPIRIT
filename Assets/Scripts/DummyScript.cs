using UnityEngine;
using System.Collections;

public class DummyScript : MonoBehaviour {
    private Animator dummyanim;
	// Use this for initialization
	void Start () {

        dummyanim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision _col)
    {
        
        if (_col.collider.tag == "dagger")
        {
            dummyanim.SetTrigger("Gethit");
            VillageDialogue.hitDummy = true;
        }
    }
}
