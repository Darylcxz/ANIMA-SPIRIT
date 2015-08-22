using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.IO;

public class DialogueScript : MonoBehaviour
{
	public TextAsset dialogue;
    private XmlNode texttoshow;
    public Text dialogs;
    public Image characterpic;
    public Sprite chara1;
    public Sprite chara2;
    public Sprite chara3;
    public Sprite chara4;
    public Image textbox;
    private string charaname;
    private RaycastHit hit;
    private bool textcomplete = false;
    private bool istalking = false;
    public AudioSource beepsound;
    public AudioClip beep;
    public static string NPCname;
    public static bool cantalk;
    private Vector3 center;
    private Vector3 side1;
    private Vector3 side2;
    public MovementController _mScript;
	public static int _seqNum;
	
	public virtual void Start() {
        textbox.enabled = false;
        dialogs.enabled = false;
        characterpic.enabled = false;
        _mScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();

	}

    public virtual void Update()
    {
        center = transform.position + new Vector3(0, 0.5f, 0);
        side1 = center + new Vector3(0.2f, 0, 0);
        side2 = center + new Vector3(-0.2f, 0, 0);
        Debug.DrawRay(center, transform.forward * 1);
        Debug.DrawRay(side1, transform.forward * 1);
        Debug.DrawRay(center, transform.forward * 1);
        if (Physics.Raycast(center, transform.forward, out hit, 1) && !istalking || Physics.Raycast(side1, transform.forward, out hit, 1) && !istalking || Physics.Raycast(side2, transform.forward, out hit, 1) && !istalking)
        //if (cantalk)
        {
            if (Input.GetButtonDown("Action") && hit.collider.tag == "talking")
            {
                NPCname = hit.collider.name;
                string textData = dialogue.text;
                ParseDialogue(textData);
            }
        }

        else if (Input.GetButtonDown("Action") && textcomplete && istalking)
        {
            textcomplete = false;
            if(texttoshow.NextSibling != null)
            {
                //beepsound.PlayOneShot(beep);
                string tempstr = Nextnode(texttoshow);
                StartCoroutine(Printletters(tempstr));
				Debug.Log(_seqNum + "if");
				_seqNum++;
            }

            else
            {
                CheckNames();
                textbox.enabled = false;
                dialogs.enabled = false;
                characterpic.enabled = false;
                istalking = false;
                _mScript.bForcedMove = false;
				_seqNum = 0;
				Debug.Log(_seqNum + "else");
            }
        }

        else if (Input.GetButtonDown("Action") && !textcomplete && istalking)
        {
            textcomplete = true;
        }
    }

    public void ParseDialogue(string xmlData)
    {
        _mScript.bForcedMove = true;
        textbox.enabled = true;
        dialogs.enabled = true;
        characterpic.enabled = true;
        istalking = true;

		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load(new StringReader(xmlData));
		
		string xmlPathPattern = "//Dialogue";
		XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode node in myNodeList)
        {
            string tempstr = FirstDialogue(node);
            StartCoroutine(Printletters(tempstr));
        }
            
	}
	
	private string FirstDialogue(XmlNode node) {
        XmlNode thenode = node[NPCname];
        XmlNode newtext = thenode.FirstChild;
        Checkchara(newtext);
        texttoshow = newtext;
        return newtext.InnerXml;
	}

    private string Nextnode(XmlNode node)
    {
        XmlNode newtextnext = node.NextSibling;
        Checkchara(newtextnext);
        texttoshow = newtextnext;
        return newtextnext.InnerXml;
    }

    private void Checkchara(XmlNode node)
    {
        string character = node.Attributes["character"].Value;
        switch(character)
        {
            case "Gulnaz":
                print("reading from gulnaz");
                characterpic.sprite = chara1;
                break;

            case "Serik":
                print("reading from serik");
                characterpic.sprite = chara2;
                break;

            case "Temir":
                print("reading from Ryuunosuke");
                characterpic.sprite = chara4;
                break;

            case "Ruslan":
                print("reading from Rockman");
                characterpic.sprite = chara3;
                break;

            default:
                print("reading from someone else");
                characterpic.sprite = null;
                break;

        }
    }

    IEnumerator Printletters(string sentence)
    {
        string str = "";
        for (int i = 0; i < sentence.Length; i++)
        {
            str += sentence[i];
            if (i == sentence.Length - 1)
            {
                print("truuuuuueeeee");
                textcomplete = true;
            }

            if(textcomplete == true)
            {
                str = sentence;
                i = sentence.Length;
            }
            dialogs.text = str;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public virtual void CheckNames()
    {
        Debug.Log("checking and changing names");
    }
}