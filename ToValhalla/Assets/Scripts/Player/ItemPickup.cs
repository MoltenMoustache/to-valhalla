using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemIcon;
        name = item.itemName;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }
    }

    void PickupItem()
    {
        bool wasPickedUp = Inventory.instance.AddItem(item);

        //if the AddItem function in Inventory script returns true, destroy the gameobject containing this item
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
