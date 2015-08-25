using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VillageDialogue : DialogueScript {

    public GameObject ruslan;
    public GameObject temir;
    public GameObject serik;
    public GameObject inzhu;
    public Transform newpos;
    public Image tutImage;
    public Sprite pressb;
    public Sprite analogstick;
    public static bool hitDummy = false;
    public static bool interactOn = false;
    public GameObject sword;

    private bool ruslan2;
    private short serikcount = 0;


    public override void Start()
    {
        base.Start();
        sword.SetActive(false);
        NPCname = "Exittent";
        string textData = dialogue.text;
        ParseDialogue(textData);
        tutImage.enabled = false;
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

        if(Checkhay.got2hay && Input.GetButtonDown("Action") && serik.name == "Serik4")
        {
            serik.name = "Serik5";
        }

        if(interactOn)
        {
            tutImage.enabled = true;
            tutImage.sprite = pressb;
            Invoke("TutorialOff", 5);
            interactOn = false;
        }

        if(NPCname == "Temir" && _seqNum == 4)
        {
            sword.SetActive(true);
        }
    }

    public override void CheckNames()
    {
        base.CheckNames();
        if(NPCname == "Ruslan")
        {
            ruslan.name = "Ruslan2";
        }

        else if(NPCname == "Exittent")
        {
            tutImage.enabled = true;
            tutImage.sprite = analogstick;
            Invoke("TutorialOff", 5);
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

        else if(NPCname == "Serik4")
        {
            tutImage.enabled = true;
            Invoke("TutorialOff", 5);
        }

        else if(NPCname == "Temir")
        {
            temir.name = "Temir2";
        }

        else if(NPCname == "Temir3")
        {
            serik.transform.position = newpos.position;
            serik.name = "Serik4";
        }

        else if(NPCname == "Inzhu")
        {
            inzhu.name = "Inzhu2";
        }


    }

    void TutorialOff()
    {
        if(tutImage.enabled)
        {
            tutImage.enabled = false;
        }
        
    }


}
