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
    public Sprite[] chara1;
    public Sprite[] chara2;
    public Sprite chara3;
    public Sprite chara4;
    public Sprite chara5;
    public Sprite[] chara6;
    public Image textbox;
    private string charaname;
    private RaycastHit hit;
    private RaycastHit hit2;
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
    //private bool faceme = false;
    public Text showname;
    public Animator charanim;
	public Animator serikAnim;
	
	public virtual void Start() {
        textbox.enabled = false;
        dialogs.enabled = false;
        characterpic.enabled = false;
        showname.enabled = false;
        _mScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        charanim = GameObject.Find("Character").GetComponent<Animator>();
		serikAnim = GameObject.FindGameObjectWithTag("SerikGhost").GetComponent<Animator>();

	}

    public virtual void Update()
    {
        center = transform.position + new Vector3(0, 0.5f, 0);
        side1 = center + new Vector3(0.2f, 0, 0);
        side2 = center + new Vector3(-0.2f, 0, 0);
        Debug.DrawRay(center, transform.forward * 5);
        Debug.DrawRay(side1, transform.forward * 5);
        Debug.DrawRay(side2, transform.forward * 5);

        if (Physics.Raycast(center, transform.forward, out hit, 1) && !istalking || Physics.Raycast(side1, transform.forward, out hit, 1) && !istalking || Physics.Raycast(side2, transform.forward, out hit, 1) && !istalking)
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
                //faceme = false;
                textbox.enabled = false;
                dialogs.enabled = false;
                characterpic.enabled = false;
                showname.enabled = false;
                istalking = false;
                _mScript.bForcedMove = false;
				_seqNum = 0;
				Debug.Log(_seqNum + "else");
                charanim.SetBool("isTalking", false);
                charanim.SetBool("bVictory", false);
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
        showname.enabled = true;
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
        int expression = int.Parse(node.Attributes["expression"].Value);
        print(expression);
        switch(character)
        {
            case "Gulnaz":
                characterpic.sprite = chara1[expression];
                showname.text = character;
                charanim.SetBool("isTalking", true);
                Invoke("StopAnim", 0.2f);
                break;

            case "Serik":
                characterpic.sprite = chara2[expression];
                showname.text = character;
                break;

            case "Temir":
                characterpic.sprite = chara4;
                showname.text = character;
                break;

            case "Ruslan":
                characterpic.sprite = chara3;
                showname.text = character;
                break;

            case "Inzhu":
                characterpic.sprite = chara5;
                showname.text = character;
                break;

            case "GhostSerik":
                characterpic.sprite = chara6[expression];
                showname.text = "Serik";
				serikAnim.SetBool("bSerikTalk", true);
                Invoke("StopAnim", 0.2f);
                break;

            default:
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
            yield return new WaitForSeconds(0.04f);
        }
    }

    public virtual void CheckNames()
    {
        Debug.Log("checking and changing names");
    }

    void StopAnim()
    {
        charanim.SetBool("isTalking", false);
        charanim.SetBool("bVictory", false);
		serikAnim.SetBool("bSerikTalk", false);
    }
}