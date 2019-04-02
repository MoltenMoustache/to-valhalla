using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Firebolt", menuName = "Spell/Wizard/Firebolt")]
public class Firebolt : Spell
{
    // Start is called before the first frame update
    void Start()
    {
        spellClass = Class.Wizard;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Cast()
    {
        Debug.Log("Thoomp, Firebolt.");
    }
}
