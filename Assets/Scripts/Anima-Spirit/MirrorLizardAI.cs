using UnityEngine;
using System.Collections;

public class MirrorLizardAI : AIbase {
    public GameObject mirror;
    private BoxCollider mirrorcollider;
	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        origin = gameObject.transform.position;
        mirrorcollider = mirror.GetComponent<BoxCollider>();
	}

    protected override void ActivateAbility()
    {
        
    }

    protected override void PassiveAbility()
    {
        if(GameControl.spiritmode == true)
        {
            mirrorcollider.enabled = true;
        }
        else
        {
            mirrorcollider.enabled = false;
        }

    }
}
