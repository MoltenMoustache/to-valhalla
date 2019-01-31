using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{

    public float armourModifier;
    public float damageModifier;
    public bool isEnchanted;

    public EquipmentSlot equipSlot;
    

    public override void UseItem()
    {
        base.UseItem();
        EquipmentManager.instance.EquipItem(this);
        Inventory.instance.RemoveItem(this);

        EquipmentManager.instance.totalArmourRating += armourModifier;
    }

    public void Enchantment()
    {

    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Trinket }
