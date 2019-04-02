using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Eldritch Blast", menuName = "Spell/Warlock/Eldritch Blast")]
public class EldritchBlast : Spell
{
    // Start is called before the first frame update
    void Start()
    {
        spellClass = Class.Warlock;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Cast()
    {
        Debug.Log("ELDRITCH BLAST!");
    }
}
