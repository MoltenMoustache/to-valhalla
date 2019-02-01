using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Player/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public Sprite skillIcon;
    public string skillDescription;
    public int runeCost;

    public SkillSO requiredSkill;
    public SkillSO counterSkill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
