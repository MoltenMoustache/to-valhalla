using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{

    public SkillSO containedSkill;
    public GameObject button;
    public Image image;
    public TextMeshProUGUI skillText;
    bool isLocked = false;
    bool isUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = containedSkill.skillIcon;
        skillText.text = containedSkill.skillName;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().playerSkills.Contains(containedSkill.requiredSkill) || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().playerSkills.Contains(containedSkill.counterSkill))
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().playerSkills.Contains(containedSkill))
        {
            UnlockSkill();
        }

        if (isLocked && containedSkill.requiredSkill != null)
        {
            if (!isUnlocked)
            {
                button.GetComponent<Image>().color = Color.red;
                button.GetComponent<Button>().interactable = false;
            }
        }

        if (!isLocked)
        {
            if (!isUnlocked)
            {
                button.GetComponent<Image>().color = Color.white;
                button.GetComponent<Button>().interactable = true;
            }
        }


        
    }

    public void UnlockSkill()
    {
        //Debug.Log("Click!");
            if ((!isLocked || containedSkill.requiredSkill == null) && GameManager.instance.runes >= containedSkill.runeCost && button.GetComponent<Button>().interactable)
            {
            GameManager.instance.RemoveRunes(containedSkill.runeCost);
                button.GetComponent<Image>().color = Color.yellow;
                button.GetComponent<Button>().interactable = false;

                if (!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().playerSkills.Contains(containedSkill))
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().playerSkills.Add(containedSkill);
                }

                isUnlocked = true;
                isLocked = true;
            }
        }
    }
