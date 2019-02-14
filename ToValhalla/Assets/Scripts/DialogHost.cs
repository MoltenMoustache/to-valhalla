using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogHost : MonoBehaviour
{
    public string npcName;
    //string playerName = GameManager.instance.playerName;
    public string[] dialogueLines;

    [SerializeField]
    protected GameObject dialoguePanel;

    [SerializeField]
    protected TextMeshProUGUI nameText;
    [SerializeField]
    protected TextMeshProUGUI dialogueText;

    protected bool isInRange = false;
    protected bool isTalking;
    bool isDone = false;
    protected int lineNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(!isTalking && isInRange && Input.GetButtonDown("Interact"))
        {
            BeginDialogue();
        }
        else if (isTalking && isInRange && Input.GetButtonDown("Interact"))
        {
            lineNumber++;
            BeginDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isInRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isInRange = false;
            dialoguePanel.SetActive(false);
        }
    }

    protected virtual void BeginDialogue()
    {
        isTalking = true;
        dialoguePanel.SetActive(true);

        nameText.text = npcName;
        if(lineNumber > dialogueLines.Length -1)
        {
            dialogueText.text = "...";
        }
        else
        {
            dialogueText.text = dialogueLines[lineNumber];
            if(lineNumber > dialogueLines.Length)
            {

            }
        }
    }
}
