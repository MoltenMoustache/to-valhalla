using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment[] currentEquipment;
    public int totalArmourRating;
    public int currentArmourRating;

    // Start is called before the first frame update
    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

    }

    public void EquipItem(Equipment newItem)
    {
        Equipment oldItem;
        int slotIndex = (int)newItem.equipSlot;

        currentArmourRating += newItem.armourModifier;
        if(currentArmourRating > totalArmourRating)
        {
            currentArmourRating = totalArmourRating;
        }

        if(currentEquipment[slotIndex] == null)
        {
            currentEquipment[slotIndex] = newItem;
        }
        else
        {
            oldItem = currentEquipment[slotIndex];
            currentEquipment[slotIndex] = newItem;
            Inventory.instance.AddItem(oldItem);
            totalArmourRating -= oldItem.armourModifier;
        }
        if (newItem.isEnchanted)
        {
            newItem.Enchantment();
        }
    }

    public void UnequipItem(Equipment oldItem)
    {
        int slotIndex = (int)oldItem.equipSlot;

        currentEquipment[slotIndex] = null;

        if(currentArmourRating > totalArmourRating)
        {
            currentArmourRating = totalArmourRating;
        }
        if (oldItem.isEnchanted)
        {
            //oldItem.Unenchant();
        }
    }
    public void FixArmour()
    {
        currentArmourRating = totalArmourRating;
    }

    public void DamageArmour()
    {
        currentArmourRating--;
    }

}
