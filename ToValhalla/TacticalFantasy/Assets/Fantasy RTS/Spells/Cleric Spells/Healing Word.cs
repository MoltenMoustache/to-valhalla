using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cleric Spell", menuName = "Spell/Cleric/Healing Word")]
public class HealingWord : Spell
{
    
    // Start is called before the first frame update
    void Start()
    {
        spellClass = Class.Cleric;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Cast()
    {
        Debug.Log("Healing Word!");
    }
}
