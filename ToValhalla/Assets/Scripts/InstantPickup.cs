using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantPickup : MonoBehaviour
{
    [SerializeField]
    Item item;
    bool isInRange;

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemIcon;
        name = item.itemName;

        if (isInRange && Input.GetButtonDown("Interact"))
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
        if (collision.tag == "Player")
        {
            isInRange = false;
            //disable button display

        }
    }
    void PickupItem()
    {
        item.UseItem();

        Destroy(gameObject);
    }
}
