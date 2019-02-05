using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create New Rune", menuName = "Inventory/Rune")]
public class Rune : Item
{
    public int runeAmount;

    public override void UseItem()
    {
        GameManager.instance.AddRunes(runeAmount);
    }
}
