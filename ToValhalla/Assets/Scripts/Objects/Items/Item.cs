using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;

    public virtual void UseItem()
    {
        Inventory.instance.RemoveItem(this);
    }
}
