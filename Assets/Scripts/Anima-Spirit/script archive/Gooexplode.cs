using UnityEngine;
using System.Collections;

public class Gooexplode : MonoBehaviour {

	public ParticleSystem bigboom;
	public static bool onGoo;




	// Use this for initialization
	void Start () {
       // _current = gameObject.GetComponent<Renderer>().material.color;
        StartCoroutine("SelfDestruct");

	}
	
	// Update is called once per frame
	void Update () {
     
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "Pig" && Input.GetButtonDown("Action")) {
			Instantiate(bigboom,transform.position,Quaternion.identity);
			Destroy(gameObject);
			onGoo = true;
		}
        if (other.gameObject.name == "Torch")
        {
            Instantiate(bigboom, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "Pig") {
			onGoo = false;
		}
	}

    IEnumerator SelfDestruct()
    {
        Debug.Log("die");
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
 
}
