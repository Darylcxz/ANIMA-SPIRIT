using UnityEngine;
using System.Collections;

public class GooBallsScript : MonoBehaviour {

    public GameObject _spawnThis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collide)
    {
        GameObject spawnClone = Instantiate(_spawnThis, collide.gameObject.transform.position,gameObject.GetComponent<Transform>().rotation) as GameObject;
        Destroy(gameObject);
    }
}
