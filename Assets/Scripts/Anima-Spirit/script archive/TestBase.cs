using UnityEngine;
using System.Collections;

public class TestBase : AIbase {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    protected override void ActivateAbility()
    {
        //throw new System.NotImplementedException();
        
    }
    protected override void PassiveAbility()
    {
      //  throw new System.NotImplementedException();
        Debug.Log("meow");
    }
}
