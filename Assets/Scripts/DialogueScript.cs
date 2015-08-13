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
	
	private void Start() {
        textbox.enabled = false;
        dialogs.enabled = false;
        characterpic.enabled = false;
	}

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 5);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5) && !istalking)
        {
            if (Input.GetButtonDown("Action"))
            {
                charaname = hit.collider.name;
                string textData = dialogue.text;
                ParseDialogue(textData);
                istalking = true;
            }
        }

        else if (Input.GetButtonDown("Action") && textcomplete && istalking)
        {
            textcomplete = false;
            if(texttoshow.NextSibling != null)
            {
                string tempstr = Nextnode(texttoshow);
                StartCoroutine(Printletters(tempstr));
            }

            else
            {
                textbox.enabled = false;
                dialogs.enabled = false;
                characterpic.enabled = false;
                istalking = false;
            }
        }

        else if (Input.GetButtonDown("Action") && !textcomplete && istalking)
        {
            textcomplete = true;
        }
    }

    public void ParseDialogue(string xmlData)
    {
        textbox.enabled = true;
        dialogs.enabled = true;
        characterpic.enabled = true;

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
        XmlNode thenode = node[charaname];
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

            case "Ryuunosuke":
                print("reading from Ryuunosuke");
                characterpic.sprite = chara4;
                break;

            case "Rockman":
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
            beepsound.PlayOneShot(beep);
            Debug.Log("playbeep");
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
}