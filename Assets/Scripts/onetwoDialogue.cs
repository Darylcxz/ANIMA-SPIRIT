using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class onetwoDialogue : DialogueScript {
    public Image climbUI;
    GameObject vines;
	// Use this for initialization
    public override void Start()
    {
        base.Start();
        vines = GameObject.Find("ClimbVines");
        climbUI.enabled = false;
    }
	
	// Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckVines();
    }

    void CheckVines()
    {
        float disttoV = Vector3.Distance(transform.position, vines.transform.position);
        print(disttoV);
        if (disttoV <= 2.6)
        {
            climbUI.enabled = true;
        }

        else
            climbUI.enabled = false;
    }
}
