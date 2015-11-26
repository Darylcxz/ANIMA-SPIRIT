using UnityEngine;
using System.Collections;

public class MirrorLizardAI : AIbase {
    private RaycastHit kk;
    private LineRenderer laser;
    public static bool gotLight;
	// Use this for initialization
	protected override void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        origin = gameObject.transform.position;
        laser = GetComponent<LineRenderer>();
	}

    protected override void ActivateAbility()
    {
        
    }

    protected override void PassiveAbility()
    {
        if(gotLight)
        {
            if (Physics.Raycast(transform.position, transform.forward, out kk, 200))
            {
                laser.SetPosition(0, transform.position);
                laser.SetPosition(1, kk.point);

                if(kk.collider.name == "GooPoly")
                {
                    Destroy(kk.collider.gameObject);
                }
            }

            else
            {
                laser.SetPosition(0, transform.position);
                laser.SetPosition(1, transform.forward * 200);
            }
        }

        else
        {
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, transform.position);
        }
    }
}
