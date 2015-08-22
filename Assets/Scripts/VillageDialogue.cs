using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VillageDialogue : DialogueScript {

    public GameObject ruslan;
    public GameObject temir;
    public GameObject serik;
    public static bool hitDummy = false;

    private bool ruslan2;
    private short serikcount = 0;


    public override void Start()
    {
        base.Start();
        NPCname = "Exittent";
        string textData = dialogue.text;
        ParseDialogue(textData);
    }

	// Use this for initialization
	
	// Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(hitDummy && temir.name == "Temir2")
        {
            temir.name = "Temir3";
        }
    }

    public override void CheckNames()
    {
        base.CheckNames();
        if(NPCname == "Ruslan")
        {
            ruslan.name = "Ruslan2";
        }

        else if(NPCname == "Serik")
        {
            serikcount += 1;
            serik.name = "Serik2";
        }

        else if (NPCname == "Serik2")
        {
            serikcount++;
            if(serikcount >= 3)
            {
                serik.name = "Serik3";
            }
        }

        else if(NPCname == "Temir")
        {
            temir.name = "Temir2";
        }

    }
}
