using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    [SerializeField]
    GameObject skillTreeUI;

    public Transform slotsParent;
    InventorySlot[] slots;

    //item description panel
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalPress = Input.GetAxisRaw("Horizontal");
        if(horizontalPress == -1)
        {
            OpenSkillTree();
        }
    }

    public void OpenSkillTree()
    {
        skillTreeUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.inventoryItems.Count)
            {
                slots[i].AddItem(inventory.inventoryItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void UnselectSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].selectedSlot = null;
        }
        //UpdateDetails();
    }

    public void DropItem()
    {
        Item itemToDrop = null;
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].selectedSlot != null)
            {
                itemToDrop = slots[i].item;
                //UnselectSlots();
                Inventory.instance.RemoveItem(itemToDrop);
            }else if(slots[i].selectedSlot == null)
            {

            }
        }
    }

    public void UseItem()
    {
        Item itemToUse = null;
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].selectedSlot != null)
            {
                itemToUse = slots[i].item;
                itemToUse.UseItem();
            }
        }
    }

    public void UpdateDetails()
    {
        Item selectedItem = null;
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].selectedSlot != null)
            {
                selectedItem = slots[i].item;
                itemName.text = selectedItem.itemName;
                itemDescription.text = selectedItem.itemDescription;
            }
        }
        
    }
}
