﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public float healAmount;

    public override void UseItem()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().HealPlayer(healAmount);
    }
}
