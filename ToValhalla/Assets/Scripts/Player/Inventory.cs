using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region singleton
    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    //variable for inventory space, editable in inspector so dont touch this one
    public int inventorySpace = 20;
    //basically the bulk of the inventory itself
    public List<Item> inventoryItems = new List<Item>();

    //call this function to add an item to the inventory using the singleton (so Inventory.instance.AddItem(item);)
    public bool AddItem(Item item)
    {
        //if the inventory is full, return false
        if(inventoryItems.Count >= inventorySpace)
        {
            Debug.LogWarning("Inventory full!");
            return false;
        }
        else
        {
            inventoryItems.Add(item);

            if(onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }

        return true;
        
    }

    //simple enough, use this to remove an item from the inventory
    public void RemoveItem(Item item)
    {
        inventoryItems.Remove(item);
        onItemChangedCallback.Invoke();
    }
}
