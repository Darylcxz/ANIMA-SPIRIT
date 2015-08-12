using UnityEngine;
using System.Collections;

public class TestBase : AIbase {

    protected Vector3 asd;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();
        origin = gameObject.transform.position;
	}
	
	// Update is called once per frame
	
    protected override void ActivateAbility()
    {
        //throw new System.NotImplementedException();
        
    }
    protected override void PassiveAbility()
    {
      //  throw new System.NotImplementedException();
       
    }
   
}
