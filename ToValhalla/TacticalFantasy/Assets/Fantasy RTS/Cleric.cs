using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : MonoBehaviour
{
    public List<Spell> spellsKnown = new List<Spell>();
    public Spell cureWounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            spellsKnown.Add(cureWounds);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            spellsKnown[0].Cast();
        }
    }
}
