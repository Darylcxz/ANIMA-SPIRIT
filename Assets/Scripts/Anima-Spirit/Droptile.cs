using UnityEngine;
using System.Collections;

public class Droptile : MonoBehaviour {

    public float delay;
    private bool isFalling;
    public Droptrigger trigger;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	 void Update () {
        if(Droptrigger.shouldFall == true)
        {
            Droptrigger.shouldFall = false;
            print("drop");
            StartCoroutine(Drop());
            isFalling = true;
        }

        if(isFalling)
        {
            print("drop la wtf");
            transform.Translate(Vector3.up * -20 * Time.deltaTime);
        }
	}

     IEnumerator Drop()
    {
        yield return new WaitForSeconds(delay);
        print("start drop");
    }
}
