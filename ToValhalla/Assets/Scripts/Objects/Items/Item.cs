﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;

    public virtual void UseItem()
    {

    }
}
