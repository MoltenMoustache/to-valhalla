using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    InventoryUI invUI;
    public InventorySlot selectedSlot;
    public Item item;
    public Image icon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedSlot == this)
        {
            
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void SelectSlot()
    {
        invUI.UnselectSlots();
        selectedSlot = this;
        invUI.UpdateDetails();
        Color colour;
        colour = Color.gray;
        colour.a = 0.3922f;
        GetComponent<Image>().color = colour;
    }
}
