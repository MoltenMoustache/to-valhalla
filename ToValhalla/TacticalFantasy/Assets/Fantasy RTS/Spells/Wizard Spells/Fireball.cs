﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Spell/Wizard/Fireball")]
public class Fireball : Spell
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Cast()
    {
        Debug.Log("HAAADOOKEN! (Fireball)");
    }
}
