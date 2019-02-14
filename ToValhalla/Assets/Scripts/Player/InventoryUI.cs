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

    private void Awake()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = slotsParent.GetComponentsInChildren<InventorySlot>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalPress = Input.GetAxisRaw("Horizontal");
        if(horizontalPress == -1)
        {
            OpenSkillTree();
        }

        ButtonHotkey();
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
            if(slots[i].selectedSlot != null)
            {
                slots[i].selectedSlot = null;
                Color colour;
                colour = Color.white;
                colour.a = 0.3922f;
                slots[i].GetComponent<Image>().color = colour;
            }
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
                if(slots[i].selectedSlot.item != null)
                {
                    itemToDrop = slots[i].item;
                    //UnselectSlots();
                    Inventory.instance.RemoveItem(itemToDrop);
                    UnselectSlots();
                }
            }
        }

        UnselectSlots();
    }

    //when function is called, the item in the currently selected slot is used
    public void UseItem()
    {
        Item itemToUse = null;
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].selectedSlot != null)
            {
                if(slots[i].selectedSlot.item != null)
                {
                    itemToUse = slots[i].item;
                    itemToUse.UseItem();
                }
            }
        }

        UnselectSlots();
    }

    void ButtonHotkey()
    {
        if (this.gameObject.activeSelf && Input.GetButtonDown("Interact"))
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].selectedSlot != null && slots[i].selectedSlot.item != null)
                {
                    UseItem();
                }
            }
        } else if (this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if(slots[i].selectedSlot != null && slots[i].selectedSlot.item != null)
                {
                    DropItem();
                }
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
                if(slots[i].selectedSlot.item != null)
                {
                    selectedItem = slots[i].item;
                    itemName.text = selectedItem.itemName;
                    itemDescription.text = selectedItem.itemDescription;
                }
            }
        }
        
    }
}