using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    public string spellName;
    public Sprite spellIcon;
    public Class spellClass;
    public int castingCost;
    public float spellRange;
    public bool requiresSave;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Cast()
    {

    }
}
