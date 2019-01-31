using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    bool isInRange;

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemIcon;
        name = item.itemName;

        if(isInRange && Input.GetButtonDown("Interact"))
        {
            PickupItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = true;
            //set the button display to active (E key)
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isInRange = false;
            //disable button display

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
