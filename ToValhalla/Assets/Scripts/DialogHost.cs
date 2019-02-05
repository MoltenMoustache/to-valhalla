using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHost : MonoBehaviour
{
    //string playerName = GameManager.instance.playerName;
    public string[] dialogueLines;
    public Queue<string> dialogueLinesL;

    bool isInRange = false;
    bool isTalking;
    int lineNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isInRange = false;
        }
    }

    void BeginDialogue()
    {
        isTalking = true;
        if(lineNumber > dialogueLines.Length -1)
        {
            Debug.Log("...");
        }
        else
        {
            Debug.Log(dialogueLines[lineNumber]);
        }
    }
}
