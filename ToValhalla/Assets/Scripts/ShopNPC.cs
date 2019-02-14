using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopNPC : DialogHost
{
    [SerializeField]
    GameObject shopUI;

    bool hasMet;
    public string[] introDialogue;
    public List<Item> shopInventory = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!isTalking && isInRange && Input.GetButtonDown("Interact") && !shopUI.activeSelf)
        {
            BeginDialogue();
        }
        else if (isTalking && isInRange && Input.GetButtonDown("Interact") && !shopUI.activeSelf)
        {
            lineNumber++;
            BeginDialogue();
        }
    }

    protected override void BeginDialogue()
    {
        isTalking = true;
        dialoguePanel.SetActive(true);

        nameText.text = npcName;
        if (((lineNumber > dialogueLines.Length - 1) && hasMet) || ((lineNumber > introDialogue.Length - 1) && !hasMet))
        {
            dialoguePanel.SetActive(false);
            shopUI.SetActive(true);
            lineNumber = -1;
            hasMet = true;
        }
        else
        {
            if (hasMet)
            {
                dialogueText.text = dialogueLines[lineNumber];
                
            }
            else
            {
                dialogueText.text = introDialogue[lineNumber];
                if (lineNumber > introDialogue.Length - 1)
                {
                    hasMet = true;
                }
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        shopUI.SetActive(false);
    }
}
