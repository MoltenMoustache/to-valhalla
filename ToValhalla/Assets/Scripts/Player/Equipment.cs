using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{

    public int armourModifier;
    public int damageModifier;
    public bool isEnchanted;

    public EquipmentSlot equipSlot;
    

    public override void UseItem()
    {
        EquipmentManager.instance.EquipItem(this);

        EquipmentManager.instance.totalArmourRating += armourModifier;

        if(isEnchanted && equipSlot != EquipmentSlot.Weapon)
        {
            Enchantment();
        }
        base.UseItem();

    }

    public virtual void Enchantment()
    {

    }
}

public enum EquipmentSlot { Head, Chest, Legs, Trinket, Weapon, Offhand }
